using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrinasBlazorSample.Models;
using IrinasBlazorSample.Shared.Common;
using Microsoft.AspNetCore.Components;

namespace IrinasBlazorSample.Shared.Company.Employees
{
    public partial class CompanyEmployees
    {
        [Parameter] 
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();

        private SimpleModal EmployeeModal { get; set; } = new SimpleModal();
        
        private Employee SelectedEmployee { get; set; }

        private async Task OnViewEmployeeClickAsync(string username)
        {
            SelectedEmployee = EmployeeList.FirstOrDefault(e => e.Username == username);
            if (SelectedEmployee != null)
            {
                await EmployeeModal.ToggleModal();
                await InvokeAsync(() => StateHasChanged());
            }
        }

        private async Task OnEmployeeModalOpenAsync(string modalId)
        {
            await InvokeAsync(() => StateHasChanged());
        }

        private async Task OnEmployeeModalClosedAsync(string modalId)
        {
            SelectedEmployee = null;
            await InvokeAsync(() => StateHasChanged());
        }

    }
}
