using System.Windows;
using System.Windows.Input;
using ProjectBReadyWPF.Backend.Services;

namespace ProjectBReadyWPF.Frontend.Components
{
    public partial class PinPromptWindow : Window
    {
        private readonly AuthService _authService;
        public bool IsAuthenticated { get; private set; } = false;

        public PinPromptWindow()
        {
            InitializeComponent();
            _authService = new AuthService();
            PinEntry.Focus();
        }

        private void BtnUnlock_Click(object sender, RoutedEventArgs e)
        {
            SubmitPin();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void PinEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitPin();
            }
        }

        private void SubmitPin()
        {
            ErrorMsg.Visibility = Visibility.Collapsed;
            string pin = PinEntry.Password;

            if (_authService.ValidatePin(pin))
            {
                IsAuthenticated = true;
                DialogResult = true;
                Close();
            }
            else
            {
                ErrorMsg.Visibility = Visibility.Visible;
                PinEntry.Clear();
                PinEntry.Focus();
            }
        }
    }
}
