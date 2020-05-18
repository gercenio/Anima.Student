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
    public class CreatedEmployeeHandler : IRequestHandler<CreatedEmployeeCommandRequest, CreatedEmployeeCommandResponse>
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CreatedEmployeeHandler(IPeopleRepository peopleRepository,IUserRepository userRepository,IEmployeeRepository employeeRepository)
        {
            _peopleRepository = peopleRepository;
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }
        
        public async Task<CreatedEmployeeCommandResponse> Handle(CreatedEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreatedEmployeeCommandResponse();

            await CreatedPeopleIntoDataAsync(request.People);

            await CreatedEmployeeIntoDataAsync(request.People);

            await CreatedUserLoginIntoDataAsync(request.People);
            
            return response;
        }

        private async Task CreatedPeopleIntoDataAsync(Domain.Entities.People entity)
        {
            await _peopleRepository.AddAsync(entity.MapToDto());
        }

        private async Task CreatedEmployeeIntoDataAsync(Domain.Entities.People entity)
        {
            int peopleId = await GetPeopleIdByDocumentAsync(entity.Document);
            if (peopleId > 0)
                await _employeeRepository.AddAsync(entity.Employee.MapToDto(peopleId));
        }

        public async Task CreatedUserLoginIntoDataAsync(People entity)
        {
            int peopleId = await GetPeopleIdByDocumentAsync(entity.Document);
            if (peopleId > 0)
                await _userRepository.AddAsync(entity.User.MapToDto(peopleId));

        }
        
        private async Task<int> GetPeopleIdByDocumentAsync(string document)
        {
            return _peopleRepository.GetAllAsync(0, 1, (m => m.Document == document)).Result.Item1.SingleOrDefault().Id;
        }
    }
}