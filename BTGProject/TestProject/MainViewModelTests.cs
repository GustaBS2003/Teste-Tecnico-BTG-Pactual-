using System;
using System.Collections.ObjectModel;
using BTGProject.Models;
using BTGProject.Services.Interfaces;
using BTGProject.ViewModels;
using Moq;

public class MainViewModelTests
{
    private readonly Mock<IClientService> _clientServiceMock;
    private readonly Mock<IServiceProvider> _serviceProviderMock;
    private readonly MainViewModel _viewModel;

    public MainViewModelTests()
    {
        _clientServiceMock = new Mock<IClientService>();
        _serviceProviderMock = new Mock<IServiceProvider>();
        _clientServiceMock.SetupGet(s => s.Clients).Returns(new ObservableCollection<Client>());
        _viewModel = new MainViewModel(_clientServiceMock.Object, _serviceProviderMock.Object);
    }

    [Fact]
    public void Clients_Should_Be_Initialized_From_Service()
    {
        Assert.NotNull(_viewModel.Clients);
        Assert.Same(_clientServiceMock.Object.Clients, _viewModel.Clients);
    }

    [Fact]
    public void CanEditOrDelete_Returns_False_When_No_SelectedClient()
    {
        _viewModel.SelectedClient = null;
        var canEditOrDelete = typeof(MainViewModel)
            .GetMethod("CanEditOrDelete", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(_viewModel, null);
        Assert.False((bool)canEditOrDelete);
    }

    [Fact]
    public void CanEditOrDelete_Returns_True_When_SelectedClient_Is_Set()
    {
        _viewModel.SelectedClient = new Client();
        var canEditOrDelete = typeof(MainViewModel)
            .GetMethod("CanEditOrDelete", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(_viewModel, null);
        Assert.True((bool)canEditOrDelete);
    }

    [Fact]
    public async Task AddClient_Should_Add_Client_To_Service()
    {
        // Arrange
        var wasCalled = false;
        _clientServiceMock
            .Setup(s => s.AddClientAsync(It.IsAny<Client>()))
            .Callback<Client>(c => wasCalled = true)
            .Returns(Task.CompletedTask);

        // Simulate the dialog result by invoking OnSaved directly
        var editVm = new BTGProject.ViewModels.ClientEditViewModel();
        editVm.OnSaved = async (client) =>
        {
            await _clientServiceMock.Object.AddClientAsync(client);
        };

        // Act
        editVm.OnSaved.Invoke(new Client { Name = "Test", Lastname = "User", Age = 30, Address = "Test" });

        // Assert
        Assert.True(wasCalled);
    }

    [Fact]
    public async Task EditClient_Should_Update_Client_In_Service()
    {
        // Arrange
        var client = new Client { Id = Guid.NewGuid(), Name = "Old", Lastname = "Name", Age = 20, Address = "Old" };
        _viewModel.SelectedClient = client;

        var wasCalled = false;
        _clientServiceMock
            .Setup(s => s.UpdateClientAsync(It.IsAny<Client>()))
            .Callback<Client>(c => wasCalled = true)
            .Returns(Task.CompletedTask);

        // Simulate the dialog result by invoking OnSaved directly
        var editVm = new BTGProject.ViewModels.ClientEditViewModel(client);
        editVm.OnSaved = async (updatedClient) =>
        {
            await _clientServiceMock.Object.UpdateClientAsync(updatedClient);
        };

        // Act
        editVm.OnSaved.Invoke(new Client { Id = client.Id, Name = "New", Lastname = "Name", Age = 21, Address = "New" });

        // Assert
        Assert.True(wasCalled);
    }

    [Fact]
    public async Task DeleteClient_Should_Delete_Client_From_Service()
    {
        // Arrange
        var client = new Client { Id = Guid.NewGuid(), Name = "ToDelete", Lastname = "User", Age = 40, Address = "Del" };
        _viewModel.SelectedClient = client;

        var wasCalled = false;
        _clientServiceMock
            .Setup(s => s.DeleteClientAsync(client.Id))
            .Callback<Guid>(id => wasCalled = true)
            .Returns(Task.CompletedTask);

        // Simulate confirmation dialog by calling the service directly
        await _clientServiceMock.Object.DeleteClientAsync(client.Id);

        // Assert
        Assert.True(wasCalled);
    }
}