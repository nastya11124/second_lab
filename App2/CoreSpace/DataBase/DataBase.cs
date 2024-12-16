using App2.CoreSpace.DataBase.Interfaces;

namespace App2.CoreSpace.DataBase
{
    class DataBase : AbstractDataBase
    {
        private Dictionary<string, List<string>> catalog;

        public DataBase()
        {
            catalog = new Dictionary<string, List<string>>();
        }
        public Dictionary<string, List<string>> GetCatalog()
        {
            return catalog;
        }
        public void SetCatalog(Dictionary<string, List<string>> startCatalog)
        {
            catalog = startCatalog;
        }
    }
}




