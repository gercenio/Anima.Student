using Anima.Student.Adapter.Api.Models;
using Anima.Student.Application.Commands.Request;

namespace Anima.Student.Adapter.Api.Mappers
{
    public static class DeletarMatriculaModelMapper
    {
        public static DeleteSchoolEnrollmentCommandRequest MapToCommand(this DeletarMatriculaModel model)
        => new DeleteSchoolEnrollmentCommandRequest(model.CodGrade,model.Ra);
            
    }

    
}