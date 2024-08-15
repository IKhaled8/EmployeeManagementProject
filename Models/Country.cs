using EmployeeManagement.Models;

namespace EmployeeManagement
{
    public class Country:UserActivity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}