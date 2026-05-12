using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
using ProjectBReadyWPF.Backend.Interfaces;
using ProjectBReadyWPF.Database.DataAccess;

namespace ProjectBReadyWPF.Backend.Services
{
    public class PostgresRealTimeService : IRealTimeService, IDisposable
    {
        public event EventHandler<string>? OnTableUpdated;

        private readonly string _connectionString;
        private NpgsqlConnection? _listenerConnection;
        private CancellationTokenSource? _cts;
        private Task? _listenerTask;

        public PostgresRealTimeService()
        {
            // Use DBHelper logic to get connection string
            var dbHelper = new DBHelper();
            _connectionString = dbHelper.GetConnection().ConnectionString;
        }

        public void StartListening()
        {
            if (_listenerTask != null && !_listenerTask.IsCompleted)
            {
                // Already listening
                return;
            }

            _cts = new CancellationTokenSource();
            _listenerTask = Task.Run(() => ListenForNotificationsAsync(_cts.Token));
        }

        public void StopListening()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        private async Task ListenForNotificationsAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    using (_listenerConnection = new NpgsqlConnection(_connectionString))
                    {
                        await _listenerConnection.OpenAsync(token);

                        // Hook up the event handler
                        _listenerConnection.Notification += (o, e) =>
                        {
                            // e.Payload contains the TG_TABLE_NAME (e.g., 'inventory_items')
                            string changedTable = e.Payload;
                            
                            // Dispatch the event to the UI thread so views can safely update
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                OnTableUpdated?.Invoke(this, changedTable);
                            });
                        };

                        // Issue the LISTEN command to the database
                        using (var cmd = new NpgsqlCommand("LISTEN bready_updates;", _listenerConnection))
                        {
                            await cmd.ExecuteNonQueryAsync(token);
                        }

                        // Infinite loop waiting for notifications until cancelled or connection drops
                        while (!token.IsCancellationRequested)
                        {
                            await _listenerConnection.WaitAsync(token);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Listener was stopped intentionally
                    break;
                }
                catch (Exception ex)
                {
                    // Network issue or DB dropped connection. Wait 5 seconds and retry.
                    Console.WriteLine($"RealTimeService Error: {ex.Message}");
                    try
                    {
                        await Task.Delay(5000, token);
                    }
                    catch (OperationCanceledException)
                    {
                        break; // Stop if cancelled during delay
                    }
                }
            }
        }

        public void Dispose()
        {
            StopListening();
            _listenerConnection?.Dispose();
        }
    }
}
