using System;
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
    public class GetEmployeeByDocumentHandler : IRequestHandler<GetEmployeeByDocumentCommandRequest, GetEmployeeByDocumentCommandResponse>
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICurriculumRepository _curriculumRepository;
        private readonly ISchoolEnrollmentRepository _schoolEnrollmentRepository;
        private const decimal BaseSalary = 1200;
        private const decimal Bonus = 50;
        
        public GetEmployeeByDocumentHandler(IPeopleRepository peopleRepository
            ,IEmployeeRepository employeeRepository
            ,ICurriculumRepository curriculumRepository
            ,ISchoolEnrollmentRepository schoolEnrollmentRepository)
        {
            _employeeRepository = employeeRepository;
            _peopleRepository = peopleRepository;
            _curriculumRepository = curriculumRepository;
            _schoolEnrollmentRepository = schoolEnrollmentRepository;
        }


        public async Task<GetEmployeeByDocumentCommandResponse> Handle(GetEmployeeByDocumentCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new GetEmployeeByDocumentCommandResponse();

            var entity = await GetPeopleByDocumentAsync(request.Document);
            
            var countCurriculum = await GetTotalCountCurriculumByTeacherIdAsync(entity.Id);
            
            var countStudent = await GetTotalCountStudentByTeacherIdAsync(entity.Id);

            var saldo = CalcNetSalary(countCurriculum,countStudent);
            
            response.Result = new
            {
                codFuncionario = entity.Employee.Code,
                nome = entity.Name ,
                cpf = entity.Document,
                email = entity.Email,
                totalGrades = countCurriculum,
                totalAlunos = countStudent,
                salario = saldo
            };
            
            return response;
        }

        private async Task<decimal> CalcNetSalary(int countCurriculum, int countStudent)
        {
            int maxCountCurriculum = 10;
            
            var saldo = (((countStudent/ maxCountCurriculum)*countCurriculum)* Bonus)+BaseSalary;

            return saldo;
        }

        private async Task<Employee> GetEmployeeByPeopleIdAsync(int peopleId)
        {
            return _employeeRepository.GetAllAsync(0, 1, (m => m.PeopleId == peopleId)).Result.Item1.SingleOrDefault()
                .MapToEntity();
        }

        private async Task<People> GetPeopleByDocumentAsync(string document)
        {
            var entity = _peopleRepository.GetAllAsync(0, 1, (m => m.Document == document)).Result.Item1.SingleOrDefault()
                .MapToEntity();
            
            entity.AddEmployee(await GetEmployeeByPeopleIdAsync(entity.Id));

            return entity;
        }

        private async Task<int> GetTotalCountStudentByTeacherIdAsync(int id)
        {
            int countResult = 0;
            var listCurriculum = _curriculumRepository.GetAllAsync(0, 1, (m => m.PeopleId == id)).Result.Item1.ToList();

            foreach (var curriculum in listCurriculum)
            {
                countResult += _schoolEnrollmentRepository.GetAllAsync(0, 1, (m => m.CurriculumId == curriculum.Id))
                    .Result.Item1.Count();
            }
            
            return countResult;
        }

        private async Task<int> GetTotalCountCurriculumByTeacherIdAsync(int id)
        {
            return _curriculumRepository.GetAllAsync(0, 1, (m => m.PeopleId == id)).Result.Item1.Count();
        }
    }
}