using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.RepoLayer.DataBase.DbModel
{
    public class VEventInDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public virtual PersonInDb Creator { get; set; }
        public string Name { get; set; }
        public string Start { set; get; }
        public string End { set; get; }
        public string Description { set; get; }
        [Required]
        public virtual LocationInDb Location { get; set; }
        public string FirstReminder { get; set; }
        public string SecondReminder { get; set; }
        public HashSet<PersonInDb> Participants { get; set; } = new HashSet<PersonInDb>();

        public VEventInDb()
        {
        }
    }
}