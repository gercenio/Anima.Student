using Anima.Student.Application.Commands.Response;
using Anima.Student.Domain.Entities;
using MediatR;

namespace Anima.Student.Application.Commands.Request
{
    public class CreatedCurriculumCommandRequest : IRequest<CreatedCurriculumCommandResponse>, IRequest<CreatedCurriculumCommandRequest>
    {
        public Curriculum Curriculum { get; }

        public CreatedCurriculumCommandRequest(Curriculum curriculum)
        {
            Curriculum = curriculum;
        }
    }
}