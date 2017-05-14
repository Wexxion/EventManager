using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.RepoLayer.DataBase.DbModel
{
    public class LocationInDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Latitide { get; set; }
        public double Longtitude { get; set; }
    }
}