using IrinasBlazorSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrinasBlazorSample.Data;
using Microsoft.Extensions.DependencyInjection;
using Bogus;
using Bogus.DataSets;

namespace IrinasBlazorSample.Services
{
    public interface ICompanyService
    {
        Task<ObjectGraph> GetDemoCompanyObjectGraph();
    }

    public class CompanyService : ICompanyService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Faker _faker;

        public CompanyService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _faker = new Faker();
        }

        public async Task<ObjectGraph> GetDemoCompanyObjectGraph()
        {
            return new ObjectGraph
            {
                CompanyName = "The Demo Company",
                Employees = new List<Employee>
                {
                    new Employee
                    {
                        Username = new Faker().Person.UserName,
                        Departments = new List<Department>
                        {
                            new Department
                            {
                                DepartmentId = "PD",
                                DepartmentName = "Product Development"
                            },
                            new Department
                            {

                                DepartmentId = "EG",
                                DepartmentName = "Engineering"
                            }
                        }
                    },
                    new Employee
                    {
                        Username = new Faker().Person.UserName,
                        Departments = new List<Department>
                        {
                            new Department
                            {
                                DepartmentId = "AC",
                                DepartmentName = "Accounting"
                            },
                            new Department
                            {

                                DepartmentId = "HR",
                                DepartmentName = "Human Resources"
                            }
                        }
                    },
                    new Employee
                    {
                        Username = new Faker().Person.UserName,
                        Departments = new List<Department>
                        {
                            new Department
                            {
                                DepartmentId = "IS",
                                DepartmentName = "Inside Sales"
                            }
                        }
                    }
                },
                Clients = new List<Client>
                {
                    new Client
                    {
                        ClientId = _faker.Random.Int(1),
                        PersonalData = new PersonalData
                        {
                            DataItems = new List<PersonalDataItem>
                            {
                                new PersonalDataItem
                                {
                                    Key = DataItemType.PhoneNumber,
                                    Value = _faker.Phone.ToString(),
                                },
                                new PersonalDataItem
                                {
                                    Key = DataItemType.Email,
                                    Value = _faker.Person.Email,
                                },
                                new PersonalDataItem
                                {
                                    Key = DataItemType.Gender,
                                    Value = _faker.Person.Gender.ToString()
                                }
                            }
                        }
                    },
                    new Client
                    {
                        ClientId = _faker.Random.Int(1),
                        PersonalData = new PersonalData
                        {
                            DataItems = new List<PersonalDataItem>
                            {
                                new PersonalDataItem
                                {
                                    Key = DataItemType.PhoneNumber,
                                    Value = _faker.Phone.ToString(),
                                },
                                new PersonalDataItem
                                {
                                    Key = DataItemType.Email,
                                    Value = _faker.Person.Email,
                                },
                                new PersonalDataItem
                                {
                                    Key = DataItemType.Gender,
                                    Value = _faker.Person.Gender.ToString()
                                }
                            }
                        }
                    },
                    new Client
                    {
                        ClientId = _faker.Random.Int(1),
                        PersonalData = new PersonalData
                        {
                            DataItems = new List<PersonalDataItem>
                            {
                                new PersonalDataItem
                                {
                                    Key = DataItemType.PhoneNumber,
                                    Value = _faker.Phone.ToString(),
                                },
                                new PersonalDataItem
                                {
                                    Key = DataItemType.Email,
                                    Value = _faker.Person.Email,
                                },
                                new PersonalDataItem
                                {
                                    Key = DataItemType.Gender,
                                    Value = _faker.Person.Gender.ToString()
                                }
                            }
                        }
                    },
                    new Client
                    {
                        ClientId = _faker.Random.Int(1),
                        PersonalData = new PersonalData
                        {
                            DataItems = new List<PersonalDataItem>
                            {
                                new PersonalDataItem
                                {
                                    Key = DataItemType.PhoneNumber,
                                    Value = _faker.Phone.ToString(),
                                },
                                new PersonalDataItem
                                {
                                    Key = DataItemType.Email,
                                    Value = _faker.Person.Email,
                                },
                                new PersonalDataItem
                                {
                                    Key = DataItemType.Gender,
                                    Value = _faker.Person.Gender.ToString()
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
