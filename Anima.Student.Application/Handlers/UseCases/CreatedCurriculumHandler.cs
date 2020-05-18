using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anima.Student.Application.Commands.Request;
using Anima.Student.Application.Commands.Response;
using Anima.Student.Domain.Entities;
using Anima.Student.Infra.Data.Interfaces;
using Anima.Student.Infra.Data.Mappers;
using MediatR;

namespace Anima.Student.Application.Handlers.UseCases
{
    public class CreatedCurriculumHandler : IRequestHandler<CreatedCurriculumCommandRequest, CreatedCurriculumCommandResponse>
    {
        private readonly ICurriculumRepository _curriculumRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CreatedCurriculumHandler(ICurriculumRepository curriculumRepository
            ,IEmployeeRepository employeeRepository)
        {
            _curriculumRepository = curriculumRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<CreatedCurriculumCommandResponse> Handle(CreatedCurriculumCommandRequest request, CancellationToken cancellationToken)
        {

            var response = new CreatedCurriculumCommandResponse();

            await CreatedCurriculumIntoDataAsync(request.Curriculum);

            return response;
            
        }

        private async Task CreatedCurriculumIntoDataAsync(Curriculum entity)
        {
            int peopleId = await GetPeopleIdByEmployeeCodeAsync(entity.Employee.Code);
            if (peopleId > 0)
                await _curriculumRepository.AddAsync(entity.MapToDto(peopleId));
        }

        private async Task<int> GetPeopleIdByEmployeeCodeAsync(int code)
        {
            return _employeeRepository.GetAllAsync(0, 1, (m => m.Code == code)).Result.Item1.SingleOrDefault().PeopleId;
        }
    }
}