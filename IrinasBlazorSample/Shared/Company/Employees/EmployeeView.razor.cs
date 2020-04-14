using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrinasBlazorSample.Models;

namespace IrinasBlazorSample.Shared.Company.Employees
{
    public partial class EmployeeView
    {
        [Parameter]
        public Employee Model { get; set; }

    }
}
