namespace BTGProject.Core.Services
{
    public interface IAlertService
    {
        Task<bool> ShowAlertAsync(string title, string message, string accept, string cancel);
        Task ShowAlertAsync(string title, string message, string cancel);
    }
}