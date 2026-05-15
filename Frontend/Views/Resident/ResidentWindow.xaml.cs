using System.Windows;
using System.Windows.Input;
using ProjectBReadyWPF.Frontend.Views.Admin;
using ProjectBReadyWPF.Frontend.Components;

namespace ProjectBReadyWPF.Frontend.Views.Resident
{
    public partial class ResidentWindow : Window
    {
        public ResidentWindow()
        {
            InitializeComponent();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Hidden admin login: Ctrl + Shift + O
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.O)
            {
                var pinPrompt = new PinPromptWindow();
                pinPrompt.Owner = this;
                bool? result = pinPrompt.ShowDialog();

                if (result == true && pinPrompt.IsAuthenticated)
                {
                    var adminWindow = new AdminWindow();
                    MatchWindowGeometry(adminWindow, this);
                    adminWindow.Closed += (_, _) => Show();

                    Hide();
                    adminWindow.Show();
                }

                e.Handled = true;
            }
        }

        private static void MatchWindowGeometry(Window target, Window source)
        {
            if (source.WindowState == WindowState.Maximized)
            {
                target.WindowState = WindowState.Maximized;
                return;
            }

            var bounds = source.WindowState == WindowState.Normal
                ? new Rect(source.Left, source.Top, source.Width, source.Height)
                : source.RestoreBounds;

            target.WindowState = WindowState.Normal;
            target.WindowStartupLocation = WindowStartupLocation.Manual;
            target.Width = bounds.Width > 0 ? bounds.Width : source.Width;
            target.Height = bounds.Height > 0 ? bounds.Height : source.Height;
            target.Left = bounds.Left;
            target.Top = bounds.Top;
        }
    }
}
