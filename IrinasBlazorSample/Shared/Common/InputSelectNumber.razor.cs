using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace IrinasBlazorSample.Shared.Common
{
    public partial class InputSelectNumber<T> : InputSelect<T>
    {
        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            result = default;
            validationErrorMessage = null;

            if (!IsValidNumberType)
            {
                return base.TryParseValueFromString(value, out result, out validationErrorMessage);
            }

            var parseResult = GetParsedNumber(value);

            if (parseResult.IsValid)
            {
                result = parseResult.Value;
            }
            else
            {
                validationErrorMessage = "The chosen value is not a valid number.";
            }

            return validationErrorMessage is null;
        }


        private bool IsValidNumberType
            => typeof(T) == typeof(short) ||
               typeof(T) == typeof(int) ||
               typeof(T) == typeof(long);


        private (bool IsValid, T Value) GetParsedNumber(string value)
        {
            T parsedValue = default;
            bool isValid = false;

            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Int16:
                    if (short.TryParse(value, out var shortValue))
                    {
                        parsedValue = (T)(object)shortValue;
                        isValid = true;
                    }
                    break;
                case TypeCode.Int32:
                    if (int.TryParse(value, out var intValue))
                    {
                        parsedValue = (T)(object)intValue;
                        isValid = true;
                    }
                    break;
                case TypeCode.Int64:
                    if (long.TryParse(value, out var longValue))
                    {
                        parsedValue = (T)(object)longValue;
                        isValid = true;
                    }
                    break;
            }

            return (isValid, parsedValue);
        }
    }
}
