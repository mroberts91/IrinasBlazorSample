using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace IrinasBlazorSample.Pages
{
    public partial class LogicSeperateComponent
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        string displayMessage = "Interact with the page to change the Message";
        bool buttonDisabled = false;
        string inputText = "";
        FormModel Model = new FormModel();
        FormModel SubmittedModel = new FormModel();
        List<string> Titles = new List<string> { "Mr.", "Ms.", "Mrs.", "товарищ" };

        void ButtonClick()
        {
            displayMessage = "You clicked the button.";
        }

        void ToggleButton()
        {
            buttonDisabled = !buttonDisabled;
            displayMessage = $"Button has been {(buttonDisabled ? "Disabled" : "Enabled")}";
        }

        void InputTextChange(object evt)
        {
            var change = evt as ChangeEventArgs;
            if (change != null && change.Value != null)
            {
                displayMessage = "Text Input Value Changed";
                inputText = change.Value.ToString();
            }
        }

        void InvalidFormSubmit()
        {
            displayMessage = "The form you submitted is invalid.";
        }

        void FormSubmit(EditContext context)
        {
            SubmittedModel = (FormModel)context.Model;
            displayMessage = "You Submited a valid form.";
        }

        class FormModel
        {
            [Required]
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Title { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            public string Bio { get; set; }
        }
    }
}
