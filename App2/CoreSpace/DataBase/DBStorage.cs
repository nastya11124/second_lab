using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using App2.CoreSpace.DataBase.Interfaces;
using System.Data.Common;

namespace App2.CoreSpace.DataBase
{
    public class DBStorage : IRepository
    {
        private readonly string _connectionString;

        public DBStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task<int?> GetArtistIdByName(string artistName)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                db.Open();

                string query = @"
                SELECT Id 
                FROM Artists 
                WHERE Name = @ArtistName;
            ";

                return await db.QueryFirstOrDefaultAsync<int?>(query, new { ArtistName = artistName });
            }
        }

        public async Task<bool> CheckTrackExists(string artistName, string title)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                db.Open();

                int? artistId = await GetArtistIdByName(artistName);

                if (artistId == null)
                {
                    return false;
                }

                string query = @"
            SELECT EXISTS (
                SELECT 1 
                FROM Tracks 
                WHERE Title = @Title AND ArtistId = @ArtistId
            );
        ";

                return await db.ExecuteScalarAsync<bool>(query, new { Title = title, ArtistId = artistId });
            }
        }

        public async Task<bool> DeleteTrack(string artistName, string title)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                db.Open();

                int? artistId = await GetArtistIdByName(artistName);

                string query = @"
            DELETE FROM Tracks 
            WHERE Title = @Title AND ArtistId = @ArtistId;
        ";

                await db.ExecuteAsync(query, new { Title = title, ArtistId = artistId });

                query = @"
            SELECT EXISTS (
                SELECT 1 
                FROM Tracks 
                WHERE ArtistId = @ArtistId
            );
        ";

                if (!await db.ExecuteScalarAsync<bool>(query, new { ArtistId = artistId }))
                {
                    query = @"
                DELETE FROM Artists 
                WHERE Id = @ArtistId;
            ";

                    await db.ExecuteAsync(query, new { ArtistId = artistId });
                }

                return true;
            }
        }

        public async Task<bool> AddTrack(string artistName, string title)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                db.Open();

                string query;
                int? artistId = await GetArtistIdByName(artistName);

                if (artistId == null)
                {
                    query = @"
                    INSERT INTO Artists (Name) 
                    VALUES (@ArtistName);";

                    await db.ExecuteAsync(query, new { ArtistName = artistName });


                    artistId = await GetArtistIdByName(artistName);
                    query = @"
                    INSERT INTO Tracks (Title, ArtistId) 
                    VALUES (@Title, @ArtistId);
                ";

                    await db.ExecuteAsync(query, new { Title = title, ArtistId = artistId });
                    return true;
                }

                query = @"
                    INSERT INTO Tracks (Title, ArtistId) 
                    VALUES (@Title, @ArtistId);
                ";

                await db.ExecuteAsync(query, new { Title = title, ArtistId = artistId });
                return true;
            }
        }
        public async Task<Dictionary<string, List<string>>> SearchTracks(bool byAuthor, string criterion, int page, int pageSize)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                db.Open();

                string query;
                object parameters;
                int? artistId = await GetArtistIdByName(criterion);

                int offset = (page - 1) * pageSize;
                if (byAuthor)
                {
                    if (artistId == null)
                    {
                        return new Dictionary<string, List<string>>();
                    }

                    query = @"
                    SELECT a.Name AS ArtistName, t.Title AS TrackName
                    FROM Tracks t
                    JOIN Artists a ON t.ArtistId = a.Id
                    WHERE t.ArtistId = @ArtistId;
                ";
                    parameters = new { ArtistId = artistId, PageSize = pageSize, Offset = offset };
                }
                else
                {
                    query = @"
                    SELECT a.Name AS ArtistName, t.Title AS TrackName
                    FROM Tracks t
                    JOIN Artists a ON t.ArtistId = a.Id
                    WHERE t.Title ILIKE @Criterion;
                ";
                    parameters = new { Criterion = $"%{criterion}%", PageSize = pageSize, Offset = offset };
                    
                }

                var results = await db.QueryAsync<(string ArtistName, string TrackName)>(query, parameters);
                var tracksDictionary = new Dictionary<string, List<string>>();

                foreach (var result in results)
                {
                    string artistName = result.ArtistName;
                    string trackTitle = result.TrackName;

                    if (!tracksDictionary.ContainsKey(artistName))
                    {
                        tracksDictionary[artistName] = new List<string>();
                    }

                    tracksDictionary[artistName].Add(trackTitle);
                }

                return tracksDictionary;
            }
        }
        public async Task<bool> HasMoreResults(bool byAuthor, string criterion, int page, int pageSize)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                db.Open();

                string query;
                int? artistId = await GetArtistIdByName(criterion);
                if (byAuthor)
                {
                    if (artistId == null)
                    {
                        return false;
                    }

                    query = @"
                    SELECT COUNT(*)
                    FROM Tracks t
                    JOIN Artists a ON t.ArtistId = a.Id
                    WHERE t.ArtistId = @ArtistId
                    LIMIT @PageSize OFFSET @Offset;
                ";
                }
                else
                {
                    query = @"
                    SELECT COUNT(*)
                    FROM Tracks t
                    JOIN Artists a ON t.ArtistId = a.Id
                    WHERE t.Title ILIKE @Criterion
                    LIMIT @PageSize OFFSET @Offset;
                ";
                }

                var count = await db.ExecuteScalarAsync<int>(query, new { ArtistId = artistId, Criterion = $"%{criterion}%", PageSize = pageSize, Offset = (page - 1) * pageSize });

                return count > 0;
            }
        }
        public async Task<Dictionary<string, List<string>>> Search(int page, int pageSize)
        {
            Dictionary<string, List<string>> artistTracks = new Dictionary<string, List<string>>();
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                db.Open();

                string query = @"
                SELECT a.Name AS ArtistName, t.Title AS TrackName
                FROM artists a
                JOIN tracks t ON a.Id = t.ArtistId
                ORDER BY a.Name
                LIMIT @PageSize OFFSET @Offset";

                int offset = (page - 1) * pageSize;

                var results = await db.QueryAsync<(string ArtistName, string TrackName)>(query, new { PageSize = pageSize, Offset = offset });
                foreach (var result in results)
                {
                    if (!artistTracks.ContainsKey(result.ArtistName))
                    {
                        artistTracks[result.ArtistName] = new List<string>();
                    }
                    artistTracks[result.ArtistName].Add(result.TrackName);
                }
            }

            return artistTracks;
        }
    }  
}
