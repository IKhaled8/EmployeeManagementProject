using EmployeeManagement.Models;

namespace EmployeeManagement
{
    public class Employee : UserActivity
    {
        public int id { get; set; }

        public string EmpNo { get; set; }

        public string FirstName { get; set; }

        public string MiddelName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {MiddelName} {LastName}";

        public int PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public int? CountryId { get; set; }
        public Country? Country { get; set; }

        public DateTime DateOfBrith { get; set; }

        public string Address { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

        
        public string? CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? ModifiedById { get; set; }

        public DateTime ModifieddOn { get; set; }
    }
}
