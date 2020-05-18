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
    public class CreatedStudentHandler : IRequestHandler<CreatedStudentCommandRequest, CreatedStudentCommandResponse>
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;

        public CreatedStudentHandler(IPeopleRepository peopleRepository,IUserRepository userRepository,IStudentRepository studentRepository)
        {
            _peopleRepository = peopleRepository;
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }

        public async Task<CreatedStudentCommandResponse> Handle(CreatedStudentCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreatedStudentCommandResponse();
            
            await CreatedPeopleIntoDataAsync(request.People);
            
            await CreatedStudentIntoDataAsync(request.People);

            await CreatedUserLoginIntoDataAsync(request.People);
            
            return response;
        }

        private async Task CreatedPeopleIntoDataAsync(People entity)
        {
            await _peopleRepository.AddAsync(entity.MapToDto());
        }

        public async Task CreatedStudentIntoDataAsync(People entity)
        {
            int peopleId = await GetPeopleIdByDocumentAsync(entity.Document);
            if (peopleId > 0)
                await _studentRepository.AddAsync(entity.Student.MapToDto(peopleId));
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