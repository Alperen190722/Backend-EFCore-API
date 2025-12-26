using D36_AspNetCoreMvc2Introduction.Entities;
using D36_AspNetCoreMvc2Introduction.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace D36_AspNetCoreMvc2Introduction.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public List<Student> GetAll(string search) 
        {
            if (string.IsNullOrEmpty(search))
            {
                return _context.Students.ToList();
            }
            else
            {
                return _context.Students
                               .Where(s => s.FirstName.ToLower().Contains(search.ToLower()))
                               .ToList();
            }
        }


        public void Add(Student s) 
        {
            _context.Students.Add(s);

            _context.SaveChanges();
        }
    }
}
