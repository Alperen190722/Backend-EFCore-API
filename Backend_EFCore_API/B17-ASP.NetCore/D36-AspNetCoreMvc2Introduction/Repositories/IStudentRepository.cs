using D36_AspNetCoreMvc2Introduction.Entities;

namespace D36_AspNetCoreMvc2Introduction.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll(string search);
        void Add(Student s);
    }
}
