using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anima.Student.Infra.Data.Dtos
{
    [Table("Estudante")]
    public class StudentDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ra { get; set; }
        
        [ForeignKey("People")]
        public int PeopleId { get; set; }
        public PeopleDto People { get; set; }
        
        public DateTime InsertAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}