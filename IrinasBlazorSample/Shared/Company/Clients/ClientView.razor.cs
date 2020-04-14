using IrinasBlazorSample.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Shared.Company.Clients
{
    public partial class ClientView
    {
        
        [Parameter]
        public Client Model { get; set; }
        [Parameter]
        public EventCallback<Client> ClientUpdated { get; set; }

        public bool IsAddingItem { get; set; } = false;

        private void OnDataItemAdded(PersonalDataItem item)
        {
            Model.PersonalData.DataItems.Add(item);
            ClientUpdated.InvokeAsync(Model);
            AddItemFormCleared(true);
        }

        private void AddItemFormCleared(bool val)
        {
            IsAddingItem = false;
        }

        private void OnAddItemButtonClick()
        {
            IsAddingItem = true;
        }
    }
}
