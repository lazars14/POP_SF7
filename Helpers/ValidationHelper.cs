using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF7.Helpers
{
    class ValidationHelper
    {
        public static string Empty = "Morate da popunite polje!";
        public static string Numeric = "Polje mora da bude numericko!";
        public static string Pattern = "Neispravan format!";

        public static bool EmptyField(string property)
        {
            if(string.IsNullOrEmpty(property))
            {
                return true;
            }
            return false;
        }

        public static bool BiggerThanMaxLength(string property, int maxLength)
        {
            if (property.Length > maxLength)
            {
                return true;
            }
            return false;
        }

        public static bool numeric(string property)
        {
            return property.All(char.IsDigit);
        }

        public static bool containExact(string property, int exactLength)
        {
            if(property.Length != exactLength)
            {
                return false;
            }
            return true;
        }

        public static bool isDouble(string property)
        {
            double test;
            bool valid = double.TryParse(property, out test);

            return valid;
        }

        public static string returnMessageMaxLength(int maxLength)
        {
            return "Maksimalno " + maxLength + " karaktera!";
        }

        public static string returnMessageExactLength(int exactLength)
        {
            return "Mora da sadrzi tacno " + exactLength + " karaktera!";
        }
    }
}
