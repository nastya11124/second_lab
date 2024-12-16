
namespace App2.CoreSpace.Interfaces
{
    public interface IDataChanger
    {
        public Task<bool> AddTrack(string author, string name);
        public Task<bool> DeleteTrack(string author, string name);
    }
}