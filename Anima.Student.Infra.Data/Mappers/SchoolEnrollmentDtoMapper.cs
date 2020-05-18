using Anima.Student.Domain.Entities;
using Anima.Student.Infra.Data.Dtos;

namespace Anima.Student.Infra.Data.Mappers
{
    public static class SchoolEnrollmentDtoMapper
    {
        public static SchoolEnrollmentDto MapToDto(this SchoolEnrollment entity)
        => new SchoolEnrollmentDto()
        {
            PeopleId = entity.Student.Id,
            CurriculumId = entity.Curriculum.Id
            
        };
    }
}