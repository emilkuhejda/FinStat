using SQLite;

namespace FinStat.DataAccess.Entities
{
    [Table("InternalValue")]
    public class InternalValueEntity
    {
        [PrimaryKey, MaxLength(100)]
        public string Key { get; set; }

        [MaxLength(100)]
        public string Value { get; set; }
    }
}
