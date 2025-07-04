using BTGProject.Core.Services;
using BTGProject.Core.ViewModels;
using Moq;

public class ClientEditViewModelTests
{
    [Fact]
    public void Save_ValidData_CallsOnSaved()
    {
        // Arrange  
        var called = false;
        var alertServiceMock = new Mock<IAlertService>(); // Mock the IAlertService dependency  
        var vm = new ClientEditViewModel(alertServiceMock.Object, null); // Pass the required parameters  
        vm.Name = "John";
        vm.Lastname = "Doe";
        vm.Age = "30";
        vm.Address = "123 Street";
        vm.OnSaved = (client) => called = true;

        // Act  
        vm.SaveCommand.Execute(null);

        // Assert  
        Assert.True(called);
    }

    [Fact]
    public void Save_InvalidAge_DoesNotCallOnSaved()
    {
        var called = false;
        var alertServiceMock = new Mock<IAlertService>(); // Mock the IAlertService dependency  
        var vm = new ClientEditViewModel(alertServiceMock.Object, null); // Pass the required parameters  
        vm.Name = "John";
        vm.Lastname = "Doe";
        vm.Age = "abc";
        vm.Address = "123 Street";
        vm.OnSaved = (client) => called = true;

        vm.SaveCommand.Execute(null);

        Assert.False(called);
    }
}