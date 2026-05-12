using System;

namespace ProjectBReadyWPF.Backend.Interfaces
{
    public interface IRealTimeService
    {
        // Event fired whenever a table is updated in the database
        // The string payload is the name of the table that changed.
        event EventHandler<string> OnTableUpdated;

        // Starts the background listener loop
        void StartListening();

        // Stops the background listener loop
        void StopListening();
    }
}
