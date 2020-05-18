namespace Anima.Student.Domain.Entities
{
    public class SchoolEnrollment
    {
        public virtual People Student { get; private set; }

        public virtual Curriculum Curriculum { get; private set; }

        public void AddStudent(People people)
        {
            Student = people;
        }

        public void AddCurriculum(Curriculum c)
        {
            Curriculum = c;
        }
    }
}