using Anima.Student.Application.Commands.Response;
using Anima.Student.Domain.Entities;
using MediatR;

namespace Anima.Student.Application.Commands.Request
{
    public class CreatedEmployeeCommandRequest : IRequest<CreatedEmployeeCommandResponse>, IRequest<CreatedEmployeeCommandRequest>
    {
        public People People { get; }

        public CreatedEmployeeCommandRequest(People people)
        {
            People = people;
        }

    }
}