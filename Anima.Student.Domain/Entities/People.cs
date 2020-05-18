using Anima.Student.Domain.Enuns;

namespace Anima.Student.Domain.Entities
{
    public class People
    {
        public int Id { get; private set; }
        public string Document { get; }
        public string Name { get;  }
        public string Email { get; }
        public PeopleType Type { get; }

        public virtual Student Student { get; private set; }
        
        public virtual Employee Employee { get; private set; }

        public virtual User User { get; private set; }

        public void AddUser(User user)
        {
            User = user;
        }

        public void AddStudent(Student student)
        {
            Student = student;
        }

        public void AddEmployee(Employee employee)
        {
            Employee = employee;
        }

        public People(string document, string name,string email,PeopleType type)
        {
            Document = document;
            Name = name;
            Email = email;
            Type = type;
        }

        public void AddIdentity(int id)
        {
            Id = id;
        }

    }
}