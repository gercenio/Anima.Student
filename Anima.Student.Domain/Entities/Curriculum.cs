namespace Anima.Student.Domain.Entities
{
    public class Curriculum
    {

        public int Id { get; private set; }
        public int Code { get; }
        public virtual Employee Employee { get; private set; }
        public string Course { get; }
        public string Discipline { get; }
        public string Class { get; }

        public Curriculum(int code,string course, string @class,string discipline)
        {
            Code = code;
            Course = course;
            Class = @class;
            Discipline = discipline;
        }

        public void AddIdentity(int id)
        {
            Id = id;
        }

        public void AddEmployee(Employee people)
        {
            Employee = people;
        }
    }
}