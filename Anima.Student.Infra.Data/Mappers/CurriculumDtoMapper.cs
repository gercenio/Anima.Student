using Anima.Student.Domain.Entities;
using Anima.Student.Infra.Data.Dtos;

namespace Anima.Student.Infra.Data.Mappers
{
    public static class CurriculumDtoMapper
    {
        public static CurriculumDto MapToDto(this Curriculum entity,int peopleId)
        => new CurriculumDto()
        {
            PeopleId = peopleId,
            Class = entity.Class,
            Code = entity.Code,
            Course = entity.Course,
            Discipline = entity.Discipline
        };

        public static Curriculum MapToEntity(this CurriculumDto dto)
        {
            var result = new Curriculum(dto.Code,dto.Course,dto.Class,dto.Discipline);
            result.AddIdentity(dto.Id);
            
            return result;
        }

    }
}