using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anima.Student.Infra.Data.Dtos
{
    [Table("Usuario")]
    public class UserDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("People")]
        public int PeopleId { get; set; }
        public PeopleDto People { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        
        public DateTime InsertAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}