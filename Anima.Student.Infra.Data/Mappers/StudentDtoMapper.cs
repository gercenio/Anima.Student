using Anima.Student.Infra.Data.Dtos;

namespace Anima.Student.Infra.Data.Mappers
{
    public static class StudentDtoMapper
    {
        public static StudentDto MapToDto(this Domain.Entities.Student entity,int peopleId)
        => new StudentDto()
        {
            Ra = entity.Ra,
            PeopleId = peopleId
        };
        
    }
}