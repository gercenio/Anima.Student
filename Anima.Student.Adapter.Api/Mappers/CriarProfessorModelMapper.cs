using Anima.Student.Adapter.Api.Models;
using Anima.Student.Application.Commands.Request;
using Anima.Student.Domain.Entities;
using Anima.Student.Domain.Enuns;

namespace Anima.Student.Adapter.Api.Mappers
{
    public static class CriarProfessorModelMapper
    {
        public static CreatedEmployeeCommandRequest MapToCommand(this CriarProfessorModel model)
        {
            var entity = new People(model.Cpf,model.Nome,model.Email,PeopleType.Employee);
            entity.AddUser(new User(model.Login,model.senha));
            entity.AddEmployee(new Employee(model.Codigo));
            var result = new CreatedEmployeeCommandRequest(entity);
            return result;
        }
        
    }
}