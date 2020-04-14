using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Models
{
    public class ObjectGraph
    {
        public string CompanyName { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Client> Clients { get; set; }
    }

    public class Employee
    {
        public string Username { get; set; }
        public List<Department> Departments { get; set; }
    }

    public class Department
    {
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class Client
    {
        public int ClientId { get; set; }
        public PersonalData PersonalData { get; set; }
    }

    public class PersonalData
    {
        public List<PersonalDataItem> DataItems { get; set; }
    }

    public class PersonalDataItem
    {
        [Range(DataItemType.PhoneNumber, DataItemType.CitizenshipStatus)]
        public int Key { get; set; }
        [Required]
        public string Value { get; set; }
    }

    public static class DataItemType{
        public const int PhoneNumber = 1;
        public const int Email = 2;
        public const int SocialSecurityNumber = 3;
        public const int Gender = 4;
        public const int CitizenshipStatus = 5;
    }
}
