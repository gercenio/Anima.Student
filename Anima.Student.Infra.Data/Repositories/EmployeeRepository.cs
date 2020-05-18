using Anima.Student.Infra.Data.Dtos;
using Anima.Student.Infra.Data.Interfaces;

namespace Anima.Student.Infra.Data.Repositories
{
    public class EmployeeRepository : Repository<EmployeeDto> , IEmployeeRepository
    {
        
    }
}