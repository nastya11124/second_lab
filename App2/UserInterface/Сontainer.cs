using App2.CoreSpace.Interfaces;
using App2.CoreSpace;
using App2.CoreSpace.DataBase.Interfaces;
using App2.UserInterface.Commands;

namespace App2.UserInterface;

public class Сontainer
{
    private AbstractDataBase _dataCatalog;
    private IRepository _dataRepository;
    public Сontainer(AbstractDataBase data, IRepository repository)
    {
        _dataCatalog = data;
        _dataRepository = repository;
    }

    public async Task Run()
    {
        IDataChanger DCh = new DataChanger(_dataRepository);
        IDataSearcher DSh = new DataSearcher(_dataRepository);
        Invoker invoker = new Invoker();
        Catalog catalog = new Catalog(DCh, DSh);

        invoker.SetCommand("каталог", new ShowAllTracksCommand(catalog));
        invoker.SetCommand("удалить", new DeleteTrack(catalog));
        invoker.SetCommand("добавить", new AddTrackCommand(catalog));
        invoker.SetCommand("найти", new FindTrackCommand(catalog));
        invoker.SetCommand("выйти", new Quit());
        invoker.SetCommand("справка", new ShowInfo());


        await invoker.ExecuteCommand("справка");
        string UserCommand;
        while (true)
        {
            Console.WriteLine("Введите комманду:\n");
            UserCommand = Console.ReadLine();
            Console.WriteLine();
            await invoker.ExecuteCommand(UserCommand);
        }
    }

}
