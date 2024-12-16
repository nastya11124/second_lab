using App2.UserInterface.Commands.Interfaces;
using App2.CoreSpace;

namespace App2.UserInterface.Commands
{
	class AddTrackCommand : Command
	{
		private Catalog _catalog;
		public AddTrackCommand(Catalog catalog)
		{
			this._catalog = catalog;
		}
		public async Task execute()
		{
			Console.Write("Введите исполнителя песни: ");
			string author = Console.ReadLine();
			Console.Write("Введите название песни: ");
			string name = Console.ReadLine();
			bool isAdded = await this._catalog.AddTrackOperation(author, name);

			if (isAdded)
			{
				Console.WriteLine("трек успешно добавлен");

            }
		}
	}
}