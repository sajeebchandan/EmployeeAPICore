using System;

namespace EmployeeAPICore.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Department Department { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
