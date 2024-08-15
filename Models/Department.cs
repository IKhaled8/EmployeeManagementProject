using EmployeeManagement.Models;

namespace EmployeeManagement
{
    public class Department: UserActivity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string DepartmentName { get; set; }
    }
}