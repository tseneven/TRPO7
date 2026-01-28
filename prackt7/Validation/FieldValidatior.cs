using System.Globalization;
using System.Windows.Controls;

namespace prackt7.Validation
{
    public class FieldValidatior : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo
cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();
            if(input == string.Empty)
            {
                return new ValidationResult(false, "Поле является обязательным");
            }
            return ValidationResult.ValidResult;
        }
    }
}
