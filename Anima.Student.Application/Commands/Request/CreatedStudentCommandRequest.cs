using Anima.Student.Application.Commands.Response;
using Anima.Student.Domain.Entities;
using MediatR;

namespace Anima.Student.Application.Commands.Request
{
    public class CreatedStudentCommandRequest : IRequest<CreatedStudentCommandResponse>, IRequest<CreatedStudentCommandRequest>
    {
        public virtual People People { get; }

        public CreatedStudentCommandRequest(People people)
        {
            People = people;
        }
    }
}