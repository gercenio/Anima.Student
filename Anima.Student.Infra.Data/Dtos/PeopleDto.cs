using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anima.Student.Infra.Data.Dtos
{
    [Table("Pessoa")]
    public class PeopleDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public DateTime InsertAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }
}