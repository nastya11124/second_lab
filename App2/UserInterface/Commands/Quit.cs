using App2.UserInterface.Commands.Interfaces;

namespace App2.UserInterface.Commands
{
    class Quit : Command
    {
        public Task execute()
        {
            Console.WriteLine("����� ������ ��������");
            Console.ReadKey();
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}