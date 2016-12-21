using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF7.Validations
{
    class PhoneNumberValidationRule : ValidationRule
    {
        Regex regex1 = new Regex(@"[0-9][0-9][0-9]/[0-9][0-9][0-9]-[0-9][0-9][0-9]");
        Regex regex2 = new Regex(@"[0-9][0-9][0-9]/[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]");

        public static string CorrectPattern = " xxx/xxx-xxx ili xxx/xxx-xxxx";

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            String v = value as string;
            if (v != null && (regex1.Match(v).Success || regex1.Match(v).Success))
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Neispravan format broja telefona!");
        }
    }
}
