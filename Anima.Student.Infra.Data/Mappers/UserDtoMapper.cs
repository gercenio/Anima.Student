using Anima.Student.Domain.Entities;
using Anima.Student.Infra.Data.Dtos;

namespace Anima.Student.Infra.Data.Mappers
{
    public static class UserDtoMapper
    {
        public static UserDto MapToDto(this User entity,int peopleId)
        => new UserDto()
        {
            
            PeopleId = peopleId,
            Login = entity.Login,
            Password = entity.Password
            
        };
    }
}