using Anima.Student.Application.Commands.Response;
using MediatR;

namespace Anima.Student.Application.Commands.Request
{
    public class GetCurriculumByCodeCommandRequest : IRequest<GetCurriculumByCodeCommandResponse>, IRequest<GetCurriculumByCodeCommandRequest>
    {

        public int CodeGrade { get; }

        public GetCurriculumByCodeCommandRequest(int codeGrade)
        {
            CodeGrade = codeGrade;
        }
        
    }
}