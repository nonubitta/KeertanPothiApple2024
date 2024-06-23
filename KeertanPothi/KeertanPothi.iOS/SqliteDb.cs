using KeertanPothi.iOS;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqliteDb))]
namespace KeertanPothi.iOS
{
    public class SqliteDb : ISqliteDb
    {
        public SQLiteAsyncConnection GetSQLiteConnection()
        {
            var fileName = "banidb.db3";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentPath, fileName);
            return new SQLiteAsyncConnection(path);
        }
    }
}