using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Models;

namespace EmployeeManagement
{
     public class SystemCode: UserActivity
    {
        [Key]
        public int Id {get; set; }
        public string Code {get; set; }
        public string Description {get; set; }
    }
}