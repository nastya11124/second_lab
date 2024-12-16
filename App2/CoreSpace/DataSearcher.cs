using App2.CoreSpace.DataBase.Interfaces;
using App2.CoreSpace.Interfaces;
using System.Diagnostics;
using System.Formats.Tar;
using System.Text;

namespace App2.CoreSpace
{
    public class DataSearcher : IDataSearcher
    {
        private IRepository _dataManager;

        public DataSearcher(IRepository manager)
        {
            this._dataManager = manager;
        }

        public async Task<Dictionary<string, List<string>>> Search(string criterion, bool byAuthor, int page, int pageSize)
        {
            if (await _dataManager.HasMoreResults(byAuthor, criterion, page, pageSize))
            {
                return await this._dataManager.SearchTracks(byAuthor, criterion, page, pageSize);
            }
            else
            {
                return new Dictionary<string, List<string>>();
            }
        }

        public async Task<Dictionary<string, List<string>>> Search(int page, int pageSize)
        {
            return await this._dataManager.Search(page, pageSize);
        }
    }
}

