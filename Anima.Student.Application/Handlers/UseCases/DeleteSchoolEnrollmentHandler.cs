using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anima.Student.Application.Commands.Request;
using Anima.Student.Application.Commands.Response;
using Anima.Student.Infra.Data.Interfaces;
using MediatR;

namespace Anima.Student.Application.Handlers.UseCases
{
    public class DeleteSchoolEnrollmentHandler : IRequestHandler<DeleteSchoolEnrollmentCommandRequest, DeleteSchoolEnrollmentCommandResponse>
    {
        private readonly ISchoolEnrollmentRepository _schoolEnrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        
        public DeleteSchoolEnrollmentHandler(ISchoolEnrollmentRepository schoolEnrollmentRepository
            ,IStudentRepository studentRepository)
        {
            _schoolEnrollmentRepository = schoolEnrollmentRepository;
            _studentRepository = studentRepository;
        }
        
        public async Task<DeleteSchoolEnrollmentCommandResponse> Handle(DeleteSchoolEnrollmentCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteSchoolEnrollmentCommandResponse();
            
            await RemoveSchoolEnrollmentAsync(request.CodGrade, request.Ra);
            
            return response;
        }

        private async Task RemoveSchoolEnrollmentAsync(int codeGrade, string raNumber)
        {
            int studentId = await GetStudentIdByRaNumberAsync(raNumber);

            if (studentId > 0)
            {
                var entity = _schoolEnrollmentRepository
                    .GetAllAsync(0, 1, (m => m.CurriculumId == codeGrade && m.PeopleId == studentId)).Result.Item1
                    .SingleOrDefault();

                await _schoolEnrollmentRepository.RemoveAsync(entity);
            }
        }

        private async Task<int> GetStudentIdByRaNumberAsync(string raNumber)
        {
            return _studentRepository.GetAllAsync(0, 1, (m => m.Ra == raNumber)).Result.Item1.SingleOrDefault()
                .PeopleId;
        }

    }
}