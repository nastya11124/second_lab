using App2.UserInterface.Commands.Interfaces;
using App2.CoreSpace;

namespace App2.UserInterface.Commands
{
    class DeleteTrack : Command
    {
        private Catalog _catalog;
        public DeleteTrack(Catalog catalog)
        {
            _catalog = catalog;
        }

        public async Task execute()
        {
            Console.Write("Введите исполнителя песни: ");
            string author = Console.ReadLine();
            Console.Write("Введите название песни: ");
            string name = Console.ReadLine();
            bool isDeleted = await this._catalog.DeleteTrackOperation(author, name);

            if (isDeleted)
            {
                Console.WriteLine("трек успешно удален");
            }
        }
    }
}