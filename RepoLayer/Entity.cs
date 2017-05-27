using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.RepoLayer
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
