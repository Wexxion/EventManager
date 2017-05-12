using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.RepoLayer.DataBase.DbModel
{
    public class PersonInDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TelegramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}