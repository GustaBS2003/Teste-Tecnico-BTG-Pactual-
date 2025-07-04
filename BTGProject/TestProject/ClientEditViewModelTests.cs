using BTGProject.Models;
using BTGProject.ViewModels;
using Xunit;

public class ClientEditViewModelTests
{
    [Fact]
    public void Save_ValidData_CallsOnSaved()
    {
        // Arrange
        var called = false;
        var vm = new ClientEditViewModel();
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
        var vm = new ClientEditViewModel();
        vm.Name = "John";
        vm.Lastname = "Doe";
        vm.Age = "abc";
        vm.Address = "123 Street";
        vm.OnSaved = (client) => called = true;

        vm.SaveCommand.Execute(null);

        Assert.False(called);
    }
}