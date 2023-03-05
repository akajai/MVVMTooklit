using SQLite;

namespace MVVM.Models
{
    public class FileName
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = 0;
        public string? Name { get; set; } = string.Empty;
        public string? Desciption { get; set; } = string.Empty;
        public double Weight { get; set; } = 0d;
        public string? Remark { get; set; } = string.Empty;
    }
}
