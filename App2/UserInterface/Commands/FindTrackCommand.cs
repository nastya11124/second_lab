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
			Console.WriteLine("�� ������ �������� ������ ���������� �����: ����������� ��� ����?");
			string criterion = Console.ReadLine();
			if (criterion != "�����������" && criterion != "����")
			{
				Exceptions e = new Exceptions("������ �������� ��������");
				return;
			}
			Console.WriteLine("������� ��������");
			string name = Console.ReadLine();

			Dictionary<string, List<string>>? tmp = new Dictionary<string, List<string>>();


            int page = 1;
			const int pageSize = 10;

            while (tmp != null)
			{
                if (criterion == "�����������")
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
                        Console.WriteLine("���������� �� �������(\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("������ ���������� ���\n");
                        break;
                    }
                    
                }
                else
                {
                    Console.WriteLine("����������:");
                    foreach (var item in tmp)
                    {
                        Console.Write(item.Key + ":");
                        foreach (var track in item.Value)
                        {
                            Console.WriteLine("\t" + "-" + track);
                        }
                    }
                    Console.WriteLine("������ �������� ������ ����������? �� ��� ���?");
                    string answer = Console.ReadLine();

                    if (answer == "��")
                    {
                        page++;
                    }
                    else
                    {
                        Console.WriteLine("������");
                        Console.WriteLine();
                        break;
                    }
                    
                }

            }

		}
	}
}