using Anima.Student.Domain.Entities;
using Anima.Student.Domain.Enuns;
using Anima.Student.Infra.Data.Dtos;

namespace Anima.Student.Infra.Data.Mappers
{
    public static class PeopleDtoMapper
    {
        public static PeopleDto MapToDto(this People entity)
        => new PeopleDto()
        {
            Name = entity.Name,
            Document = entity.Document,
            Email = entity.Email
        };

        public static People MapToEntity(this PeopleDto dto)
        {
            var result = new People(dto.Document,dto.Name,dto.Email,(PeopleType)dto.Type);
            
            result.AddIdentity(dto.Id);
            
            return result;
        }
    }
}