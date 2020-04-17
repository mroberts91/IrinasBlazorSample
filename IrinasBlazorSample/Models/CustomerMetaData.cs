using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Models
{
    public class CustomerMetaData
    {

        // Allow up to 40 uppercase and lowercase 
        // characters. Use custom error.
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
        ErrorMessage = "Characters are not allowed.")]
        public string FirstName;

        // Allow up to 40 uppercase and lowercase 
        // characters. Use standard error.
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string LastName;
    }
}
