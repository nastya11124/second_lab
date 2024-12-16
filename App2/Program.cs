
using App2.CoreSpace.DataBase;
using App2.CoreSpace.DataBase.Interfaces;
using App2.UserInterface;

namespace App2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // строка подключения
            string connectionString = "Host=localhost;Username=postgres;Password=1019";
            // инициализация базы данных
            var initializer = new DatabaseInitializer(connectionString);
            initializer.Initialize();

            AbstractDataBase data = new DataBase();

            IRepository repository = new DBStorage(connectionString);
            Сontainer app = new Сontainer(data, repository);
            await app.Run();
        }
    }
}