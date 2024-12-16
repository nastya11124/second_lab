using Npgsql;
using System.Data;
using Dapper;
using App2.UserInterface;

namespace App2.CoreSpace.DataBase
{
    public class DatabaseInitializer
    {
        private string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Initialize()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                db.Open();

                // Проверка существования базы данных
                bool databaseExists = db.ExecuteScalar<bool>(@"
                SELECT EXISTS (
                    SELECT 1 
                    FROM pg_catalog.pg_database 
                    WHERE datname = 'postgres'
                );
            ");
                Console.WriteLine(databaseExists);

                // Создание базы данных, если она не существует
                if (!databaseExists)
                {
                        db.Execute(@"
                        CREATE DATABASE postgres;
                    ");
                }

                    // Создание таблиц
                    db.Execute(@"
                    CREATE TABLE IF NOT EXISTS Artists (
                        Id SERIAL PRIMARY KEY,
                        Name VARCHAR(100) NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Tracks (
                        Id SERIAL PRIMARY KEY,
                        Title VARCHAR(100) NOT NULL,
                        ArtistId INT REFERENCES Artists(Id)
                    );
                ");
                
            }
        }
    }
}
