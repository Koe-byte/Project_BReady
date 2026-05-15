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
                    var adminWindow = new AdminWindow
                    {
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };
                    adminWindow.Closed += (_, _) => Show();

                    Hide();
                    adminWindow.Show();
                }

                e.Handled = true;
            }
        }
    }
}
