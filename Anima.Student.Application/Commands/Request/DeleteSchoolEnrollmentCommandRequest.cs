using Anima.Student.Application.Commands.Response;
using MediatR;

namespace Anima.Student.Application.Commands.Request
{
    public class DeleteSchoolEnrollmentCommandRequest : IRequest<DeleteSchoolEnrollmentCommandResponse>, IRequest<DeleteSchoolEnrollmentCommandRequest>
    {
        public int CodGrade { get; }
        public string Ra { get; }

        public DeleteSchoolEnrollmentCommandRequest(int codGrade,string ra)
        {
            CodGrade = codGrade;
            Ra = ra;
        }
    }
}