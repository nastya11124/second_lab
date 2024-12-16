using App2.UserInterface.Commands.Interfaces;
using App2.CoreSpace;

namespace App2.UserInterface.Commands
{
	class FindTrackCommand : Command
	{
		private Catalog _catalog;

		public FindTrackCommand(Catalog catalog)
		{
			_catalog = catalog;
		}

		public async Task execute()
		{
			Console.WriteLine("По какому критерию хотите произвести поиск: исполнитель или трек?");
			string criterion = Console.ReadLine();
			if (criterion != "исполнитель" && criterion != "трек")
			{
				Exceptions e = new Exceptions("Введен неверный критерий");
				return;
			}
			Console.WriteLine("Введите значение");
			string name = Console.ReadLine();

			Dictionary<string, List<string>>? tmp = new Dictionary<string, List<string>>();


            int page = 1;
			const int pageSize = 10;

            while (tmp != null)
			{
                if (criterion == "исполнитель")
                {
                    tmp = await this._catalog.FilterTrackOperation(name, true, page, pageSize);
                }
                else
                {
                    tmp = await this._catalog.FilterTrackOperation(name, false, page, pageSize);
                }

                if (tmp.Count == 0)
                {
                    if (page == 1)
                    {
                        Console.WriteLine("Совпадений не найдено(\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Больше совпадений нет\n");
                        break;
                    }
                    
                }
                else
                {
                    Console.WriteLine("СОВПАДЕНИЯ:");
                    foreach (var item in tmp)
                    {
                        Console.Write(item.Key + ":");
                        foreach (var track in item.Value)
                        {
                            Console.WriteLine("\t" + "-" + track);
                        }
                    }
                    Console.WriteLine("Хотите получить больше совпадений? Да или нет?");
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
                    
                }

            }

		}
	}
}