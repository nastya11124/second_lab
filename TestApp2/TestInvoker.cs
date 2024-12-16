using App2.UserInterface.Commands.Interfaces;
using App2.UserInterface;
using Moq;
using System.Reflection;

namespace TestApp2
{
    public class InvokerTests
    {
        [Fact]
        public void SetCommand_AddsCommand()
        {
            // Arrange
            var invoker = new Invoker();
            var mockCommand = new Mock<Command>();

            // Act
            invoker.SetCommand("TestCommand", mockCommand.Object);
        }
        [Fact]
        public async Task ExecuteCommand_CommandExists_ExecutesCommand()
        {
            // Arrange
            var invoker = new Invoker();
            var mockCommand = new Mock<Command>();
            invoker.SetCommand("TestCommand", mockCommand.Object);

            // Act
            await invoker.ExecuteCommand("TestCommand");

            // Assert
            mockCommand.Verify(c => c.execute(), Times.Once);
        }

        [Fact]
        public async Task ExecuteCommand_CommandDoesNotExist_DoesNotExecuteCommand()
        {
            // Arrange
            var invoker = new Invoker();

            // Act
            await invoker.ExecuteCommand("NonExistentCommand");

            // Assert
            // No exception should be thrown, and no command should be executed
        }

    }
}
