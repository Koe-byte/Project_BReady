namespace ProjectBReadyWPF.Backend.Interfaces
{
    public interface IAuthService
    {
        bool ValidatePin(string inputPin);
    }
}
