using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anima.Student.Application.Commands.Request;
using Anima.Student.Application.Commands.Response;
using Anima.Student.Domain.Entities;
using Anima.Student.Infra.Data.Interfaces;
using Anima.Student.Infra.Data.Mappers;
using Anima.Student.Infra.Data.Repositories;
using MediatR;

namespace Anima.Student.Application.Handlers.UseCases
{
    public class CreatedSchoolEnrollmentHandler : IRequestHandler<CreatedSchoolEnrollmentCommandRequest, CreatedSchoolEnrollmentCommandResponse>
    {
        private readonly ISchoolEnrollmentRepository _schoolEnrollmentRepository;
        private readonly ICurriculumRepository _curriculumRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPeopleRepository _peopleRepository;

        public CreatedSchoolEnrollmentHandler(ISchoolEnrollmentRepository schoolEnrollmentRepository
            ,ICurriculumRepository curriculumRepository
            ,IPeopleRepository peopleRepository
            ,IStudentRepository studentRepository)
        {
            _schoolEnrollmentRepository = schoolEnrollmentRepository;
            _curriculumRepository = curriculumRepository;
            _peopleRepository = peopleRepository;
            _studentRepository = studentRepository;
        }

        public async Task<CreatedSchoolEnrollmentCommandResponse> Handle(CreatedSchoolEnrollmentCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreatedSchoolEnrollmentCommandResponse();

            await CreatedSchoolEnrollmentIntoDataAsync(request.CodeGrade, request.RaNumber);

            return response;
        }

        private async Task CreatedSchoolEnrollmentIntoDataAsync(int codeGrade, string raNumber)
        {
            var Student = GetStudentByRaNumberAsync(raNumber).Result;
            var Curriculum = GetCurriculumByCodeAsync(codeGrade).Result;

            if (Student != null && Curriculum != null)
            {
                var entity = new SchoolEnrollment();
                entity.AddStudent(Student);
                entity.AddCurriculum(Curriculum);

                await _schoolEnrollmentRepository.AddAsync(entity.MapToDto());
            }
        }

        private async Task<Curriculum> GetCurriculumByCodeAsync(int code)
        {
            return _curriculumRepository.GetAllAsync(0, 1, (m => m.Code == code)).Result.Item1.SingleOrDefault().MapToEntity();
        }

        private async Task<People> GetStudentByRaNumberAsync(string raNumber)
        {
            var id = _studentRepository.GetAllAsync(0, 1, (m => m.Ra == raNumber)).Result.Item1.SingleOrDefault()
                .PeopleId;

            return _peopleRepository.GetByIdAsync(id).Result.MapToEntity();
        }
    }
}