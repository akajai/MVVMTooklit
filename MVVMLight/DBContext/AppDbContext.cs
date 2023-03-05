using MVVMLight.Models;
using SQLite;

namespace MVVM.DBContext
{
    public class AppDbContext : SQLiteConnection
    {
        public AppDbContext(string path) : base(path)
        {
            CreateTable<FileName>();
        }

    }
}
