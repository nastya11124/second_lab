using App2.UserInterface.Commands.Interfaces;
using App2.CoreSpace;
using System.Text;


namespace App2.UserInterface.Commands
{
    class ShowAllTracksCommand : Command
    {
        private Catalog _catalog;
        public ShowAllTracksCommand(Catalog catalog)
        {
            _catalog = catalog;
        }

        public async Task execute()
        {
            Dictionary<string, List<string>> tmp;
            int page = 1;
            int pageSize = 10;

            tmp = await this._catalog.ShowAllTracksOperation(page, pageSize);
            while (tmp != null)
            {
                if (tmp.Count == 0)
                {
                    if (page == 1)
                    {
                        Console.WriteLine("Каталог пуст(\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Больше нет данных\n");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("КАТАЛОГ:");
                    foreach (var item in tmp)
                    {
                        Console.WriteLine(item.Key + ":");
                        foreach (var track in item.Value)
                        {
                            Console.WriteLine("\t" + "-" + track);
                        }
                    }

                    Console.WriteLine("Продолжить вывод? Да или нет?");
                    string answer = Console.ReadLine();

                    if (answer == "да")
                    {
                        page++;
                    }
                    else
                    {
                        Console.WriteLine("Хорошо");
                        Console.WriteLine();
                        break;
                    }
                    tmp = await this._catalog.ShowAllTracksOperation(page, pageSize);
                }
            }
            
        }
    }
}