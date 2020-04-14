using IrinasBlazorSample.Models;
using IrinasBlazorSample.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Pages
{
    public partial class CompanyOverview
    {
        private bool isLoading = true;
        
        [Inject] public ICompanyService CompanyService {get; set;}


        public ObjectGraph Company { get; set; }

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await LoadCompanyAsync();
            isLoading = false;
        }

        private async Task LoadCompanyAsync()
        {
            Company = await CompanyService.GetDemoCompanyObjectGraph();
        }
    }
}
