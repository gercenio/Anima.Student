using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anima.Student.Infra.Data.Dtos
{
    [Table("Grade")]
    public class CurriculumDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Code { get; set; }
        public string Course { get; set; }
        public string Discipline { get; set; }
        public string Class { get; set; }
        [ForeignKey("People")]
        public int PeopleId { get; set; }
        public PeopleDto People { get; set; }
        public DateTime InsertAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}