using Anima.Student.Domain.Entities;
using Anima.Student.Infra.Data.Dtos;

namespace Anima.Student.Infra.Data.Mappers
{
    public static class EmployeeDtoMapper
    {
        public static EmployeeDto MapToDto(this Employee entity,int peopleId)
        => new EmployeeDto()
        {
            Code = entity.Code,
            PeopleId = peopleId
        };

        public static Employee MapToEntity(this EmployeeDto dto)
        {
            var result = new Employee(dto.Code);
            
            
            return result;
        }


    }
}