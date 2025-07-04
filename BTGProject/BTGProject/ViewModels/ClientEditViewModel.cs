using BTGProject.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Net;
using System.Windows.Input;
using System.Xml.Linq;

namespace BTGProject.ViewModels
{
    public partial class ClientEditViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string lastname;

        [ObservableProperty]
        private string age;

        [ObservableProperty]
        private string address;

        public string PageTitle { get; }

        private readonly Guid? _clientId;

        public Action<Client> OnSaved { get; set; }
        public Action OnCanceled { get; set; }

        public ClientEditViewModel(Client? client = null)
        {
            if (client != null)
            {
                _clientId = client.Id;
                Name = client.Name;
                Lastname = client.Lastname;
                Age = client.Age.ToString();
                Address = client.Address;
                PageTitle = "Edit Client";
            }
            else
            {
                PageTitle = "Add Client";
            }
        }

        [RelayCommand]
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Lastname))
            {
                Application.Current.MainPage.DisplayAlert("Validation Error", "Please enter valid name and last name.", "OK");
                return;
            }
            if (!int.TryParse(Age, out int ageValue) ||
                ageValue < 0)
            {
                Application.Current.MainPage.DisplayAlert("Validation Error", "Please enter valid age.", "OK");
                return;
            }

            var client = new Client
            {
                Id = _clientId ?? Guid.NewGuid(),
                Name = Name,
                Lastname = Lastname,
                Age = ageValue,
                Address = Address
            };
            OnSaved?.Invoke(client);
        }

        [RelayCommand]
        private void Cancel()
        {
            OnCanceled?.Invoke();
        }
    }
}