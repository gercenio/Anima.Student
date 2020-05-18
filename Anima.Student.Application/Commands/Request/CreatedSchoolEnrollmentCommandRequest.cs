using Anima.Student.Application.Commands.Response;
using Anima.Student.Domain.Entities;
using MediatR;

namespace Anima.Student.Application.Commands.Request
{
    public class CreatedSchoolEnrollmentCommandRequest : IRequest<CreatedSchoolEnrollmentCommandResponse>, IRequest<CreatedSchoolEnrollmentCommandRequest>
    {

        public int CodeGrade { get; }
        public string RaNumber { get; }
        
        public CreatedSchoolEnrollmentCommandRequest(int codeGrade,string raNumber)
        {
            CodeGrade = codeGrade;
            RaNumber = raNumber;
        }
    }
}