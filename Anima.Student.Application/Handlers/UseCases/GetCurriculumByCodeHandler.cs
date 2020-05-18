using System.Threading;
using System.Threading.Tasks;
using Anima.Student.Application.Commands.Request;
using Anima.Student.Application.Commands.Response;
using Anima.Student.Infra.Data.Interfaces;
using MediatR;

namespace Anima.Student.Application.Handlers.UseCases
{
    public class GetCurriculumByCodeHandler : IRequestHandler<GetCurriculumByCodeCommandRequest, GetCurriculumByCodeCommandResponse>
    {
        private readonly ICurriculumRepository _curriculumRepository;

        public GetCurriculumByCodeHandler(ICurriculumRepository curriculumRepository)
        {
            _curriculumRepository = curriculumRepository;
        }

        public Task<GetCurriculumByCodeCommandResponse> Handle(GetCurriculumByCodeCommandRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}