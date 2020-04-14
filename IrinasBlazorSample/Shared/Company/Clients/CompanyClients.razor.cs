using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrinasBlazorSample.Models;
using IrinasBlazorSample.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace IrinasBlazorSample.Shared.Company.Clients
{
    public partial class CompanyClients
    {
        [Parameter] 
        public List<Client> ClientList { get; set; } = new List<Client>();
        private SimpleModal ClientModal { get; set; } = new SimpleModal();
        private PersonalDataItem NewDataItemModel { get; set; } = new PersonalDataItem();

        private Client SelectedClient { get; set; }

        private async Task OnViewClientClickAsync(int clientId)
        {
            SelectedClient = ClientList.FirstOrDefault(c => c.ClientId == clientId);
            if (SelectedClient != null)
            {
                await ClientModal.ToggleModal();
                await InvokeAsync(() => StateHasChanged());
            }
        }

        private async Task OnClientModalOpenAsync(string modalId)
        {
            await InvokeAsync(() => StateHasChanged());
        }

        private async Task OnClientModalClosedAsync(string modalId)
        {
            SelectedClient = null;
            await InvokeAsync(() => StateHasChanged());
        }

        //private async Task AddClientDataItem(EditContext context)
        //{
        //    var model = context.Model as PersonalDataItem;
        //    var client = SelectedClient;
        //    if (client != null && model != null)
        //    {
        //        client.PersonalData.DataItems.Add(model);
        //        await ClientModal.ToggleModal();
        //        NewDataItemModel = new PersonalDataItem();
        //        await InvokeAsync(() => StateHasChanged());
        //    }
        //}

        private void SelectedClientUpdated(Client client)
        {
            var c = ClientList.FirstOrDefault(x => x.ClientId == client.ClientId);
            c.PersonalData.DataItems = client.PersonalData.DataItems;
            StateHasChanged();
        }
    }
}
