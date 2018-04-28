using System.ComponentModel.DataAnnotations.Schema;

namespace MfaCsvUploader.Data.Entities
{
    public class CsvTeacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public string Gender { get; set; }
    }
}