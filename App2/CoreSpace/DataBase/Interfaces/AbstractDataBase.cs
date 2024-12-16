
namespace App2.CoreSpace.DataBase.Interfaces
{
	public interface AbstractDataBase
	{
		public Dictionary<string, List<string>> GetCatalog();
		public void SetCatalog(Dictionary<string, List<string>> catalog);
	}
}
