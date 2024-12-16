
using App2.CoreSpace.Interfaces;

namespace App2.CoreSpace
{
    public class Catalog
    {
        private IDataChanger _dataChanger;
        private IDataSearcher _dataSearcher;

        public Catalog(IDataChanger DCh, IDataSearcher DSh)
        {
            this._dataChanger = DCh;
            this._dataSearcher = DSh;
        }
        public async Task<bool> AddTrackOperation(string author, string name)
        {
            return await this._dataChanger.AddTrack(author, name);
        }

        public async Task<Dictionary<string, List<string>>> ShowAllTracksOperation(int page, int pageSize)
        {
            return await this._dataSearcher.Search(page, pageSize);
        }

        public async Task<Dictionary<string, List<string>>> FilterTrackOperation(string criterion, bool byAuthor, int page, int pageSize)
        {
            return await this._dataSearcher.Search(criterion, byAuthor, page, pageSize);
        }
        public async Task<bool> DeleteTrackOperation(string author, string name)
        {
            return await this._dataChanger.DeleteTrack(author, name);
        }
    }
}
    
