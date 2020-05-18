using Anima.Student.Adapter.Api.Models;
using Anima.Student.Application.Commands.Request;
using Anima.Student.Domain.Entities;

namespace Anima.Student.Adapter.Api.Mappers
{
    public static class CriarGradeModelMapper
    {
        public static CreatedCurriculumCommandRequest MapToCommand(this CriarGradeModel model)
        {
            var entity = new Curriculum(model.CodGrade,model.Curso,model.Turma,model.Disciplina);
            entity.AddEmployee(new Employee(model.CodFuncionario));
            var result = new CreatedCurriculumCommandRequest(entity);

            return result;
        }
    }
}