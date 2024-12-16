using App2.UserInterface;
using App2.CoreSpace.Interfaces;
using App2.CoreSpace.DataBase.Interfaces;

namespace App2.CoreSpace
{
    public class DataChanger : IDataChanger
    {
        private IRepository _dataManager;

        public DataChanger(IRepository manager)
        {
            this._dataManager = manager;

        }
        public async Task<bool> AddTrack(string author, string track)
        {
            if (await this._dataManager.CheckTrackExists(author, track))
            {
                Exceptions e = new Exceptions("Введенного вами трек уже есть в каталоге");
                return false;
            }
            return await this._dataManager.AddTrack(author, track);
        }

        public async Task<bool> DeleteTrack(string author, string track)
        {
            if (await this._dataManager.CheckTrackExists(author, track))
            {
                return await this._dataManager.DeleteTrack(author, track);
            }
            Exceptions e = new Exceptions("Введенного вами трека нет в каталоге");
            return false;
        }
    }
}

