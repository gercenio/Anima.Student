using Anima.Student.Adapter.Api.Models;
using Anima.Student.Application.Commands.Request;
using Anima.Student.Domain.Entities;

namespace Anima.Student.Adapter.Api.Mappers
{
    public static class CriarMatriculaModelMapper
    {

        public static CreatedSchoolEnrollmentCommandRequest MapToCommand(this CriarMatriculaModel model)
        => new CreatedSchoolEnrollmentCommandRequest(model.CodGrade,model.Ra);
        
    }
}