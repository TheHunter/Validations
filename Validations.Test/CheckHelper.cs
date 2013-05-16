using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validations.Test.Domain;


namespace Validations.Test
{
    public class CheckHelper
    {
        public static bool CheckIdentifier(Salesman cons)
        {
            return cons.IdentityCode >= 100;
        }

        public static bool CheckStringTitleCase(string name)
        {
            return name.Substring(0, 1) == name.Substring(0, 1).ToUpper();
        }

        public static bool CheckEmail(string email)
        {
            return email.Contains("@");
        }
    }
}
