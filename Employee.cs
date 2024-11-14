using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB
{
    internal class Employee
    {
        public int employeeID { get; set; }

        private string firstName;
        public string FirstName { 
            get { return firstName; } 
            set{ if (value != "") firstName = value; } }

        private string lastName;
        public string LastName { 
            get { return lastName; }
            set { if (value != "") lastName = value; } }

        private string email;
        public string Email{
            get { return email; }
            set { if (value != "") email = value; }}

        private DateTime dateOfBirth;
        public DateTime DateOfBirth { 
            get { return dateOfBirth; }
            set { dateOfBirth = value; } }

        private decimal salary;
        public decimal Salary{
            get { return salary; }
            set { salary = value; }}

        public Employee() { }

        public Employee(int employeeID, string firstName, string lastName, string email, DateTime dateOfBirth, decimal salary)
        {
            this.employeeID = employeeID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.dateOfBirth = dateOfBirth;
            this.salary = salary;
        }

        public override string ToString()
        {
            return $"{employeeID}\t{firstName} {lastName}\t{email}\t{dateOfBirth.Day}.{dateOfBirth.Month}.{dateOfBirth.Year}\t{salary}";
        }

        public string ToDBInsertString()
        {
            return $"'{firstName}','{lastName}','{email}','{dateOfBirth}','{salary}'";
        }

        public string ToDBUpdateString()
        {
            return $"FirstName='{firstName}',LastName='{lastName}',Email='{email}',DateOfBirth='{dateOfBirth}',salary='{salary}'";
        }
    }
}
