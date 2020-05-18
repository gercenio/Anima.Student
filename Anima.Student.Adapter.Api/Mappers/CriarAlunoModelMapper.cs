using Anima.Student.Adapter.Api.Models;
using Anima.Student.Application.Commands.Request;
using Anima.Student.Application.Handlers.UseCases;
using Anima.Student.Domain.Entities;
using Anima.Student.Domain.Enuns;

namespace Anima.Student.Adapter.Api.Mappers
{
    public static class CriarAlunoModelMapper
    {
        public static CreatedStudentCommandRequest MapToCommand(this CriarAlunoModel model)
        {
            var entity = new People(model.Cpf,model.Nome,model.Email,PeopleType.Student);
            entity.AddUser(new User(model.Login,model.senha));
            entity.AddStudent(new Domain.Entities.Student(model.Ra));
            var result = new CreatedStudentCommandRequest(entity);

            return result;
        }
    }
}