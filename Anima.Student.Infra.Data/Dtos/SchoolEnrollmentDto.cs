using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anima.Student.Infra.Data.Dtos
{
    [Table("Matricula")]
    public class SchoolEnrollmentDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Student")]
        public int PeopleId { get; set; }
        public PeopleDto Student { get; set; }

        [ForeignKey("Curriculum")]
        public int CurriculumId { get; set; }
        public CurriculumDto Curriculum { get; set; }
        
        public DateTime InsertAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}