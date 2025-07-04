using CommunityToolkit.Mvvm.ComponentModel;

namespace BTGProject.Core.Models
{
    public partial class Client : ObservableObject
    {
        [ObservableProperty]
        private Guid id = new Guid();

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string lastname = string.Empty;

        [ObservableProperty]
        private int age;

        [ObservableProperty]
        private string address = string.Empty;
    }
}