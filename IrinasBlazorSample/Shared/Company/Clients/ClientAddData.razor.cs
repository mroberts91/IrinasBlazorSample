using IrinasBlazorSample.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Shared.Company.Clients
{
    public partial class ClientAddData
    {

        public PersonalDataItem Model { get; set; }
        
        [Parameter]
        public EventCallback<PersonalDataItem> ValidFormSubmit { get; set; }
        [Parameter]
        public EventCallback<bool> FormInputCancelled { get; set; }

        protected override void OnInitialized()
        {
            ResetForm();
        }

        private async Task OnValidFormSubmit(EditContext context)
        {
            var model = context.Model as PersonalDataItem;
            await ValidFormSubmit.InvokeAsync(model);
            ResetForm();

        }

        private void ResetForm()
        {
            Model = new PersonalDataItem();
        }

        private void ClearForm()
        {
            ResetForm();
            FormInputCancelled.InvokeAsync(true);
        }
    }
}
