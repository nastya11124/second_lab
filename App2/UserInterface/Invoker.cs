using App2.UserInterface.Commands.Interfaces;
using App2.CoreSpace;

namespace App2.UserInterface
{
    public class Invoker
    {
        private Dictionary<string, Command> commands;

        public Invoker()
        {
            commands = new Dictionary<string, Command>();
        }

        public void SetCommand(string CommandName, Command command)
        {
            this.commands.Add(CommandName, command);
        }
        public async Task ExecuteCommand(string CommandName)
        {
            if (this.commands.ContainsKey(CommandName))
            {
                await this.commands[CommandName].execute();
            }
            else
            {
                Exceptions e = new Exceptions("Такой команды нет в списке");

            }
        }
    }
}

