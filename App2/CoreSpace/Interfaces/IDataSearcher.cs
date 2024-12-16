

namespace App2.CoreSpace.Interfaces
{
    public interface IDataSearcher
    {
        public Task<Dictionary<string, List<string>>> Search(string criterion, bool byAuthor, int page, int pageSize);
        public Task<Dictionary<string, List<string>>> Search(int page, int pageSize);
    }
}