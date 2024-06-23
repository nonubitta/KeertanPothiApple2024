
using SQLite;

namespace KeertanPothi
{
    public interface ISqliteDb
    {
        SQLiteAsyncConnection GetSQLiteConnection();
    }
}
