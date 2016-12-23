using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace POP_SF7.Validations
{
    class AccountNumberValidationRule : ValidationRule
    {
        Regex regex = new Regex(@"[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]-[0-9][0-9]");

        public static string CorrectPattern = " xxx-xxxxxxxxxxxxx-xx";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String v = value as string;
            if (v != null && regex.Match(v).Success)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Neispravan format racuna! xxx-xxxxxxxxxxxxx-xx");
        }
    }
}
