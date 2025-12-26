using D36_AspNetCoreMvc2Introduction.Entities;

namespace D36_AspNetCoreMvc2Introduction
{
    public class EmployeeListViewModel
    {
        public List<Employee> Employees { get; set; }
        public List<string> Cities { get; set; }
    }
}