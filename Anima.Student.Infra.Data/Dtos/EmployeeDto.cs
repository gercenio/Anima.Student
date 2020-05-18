using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anima.Student.Infra.Data.Dtos
{
    [Table("Funcionario")]
    public class EmployeeDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Code { get; set; }
        
        [ForeignKey("People")]
        public int PeopleId { get; set; }
        public PeopleDto People { get; set; }
        
        public DateTime InsertAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }
}