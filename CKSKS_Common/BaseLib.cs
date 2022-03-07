using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORS_RCM_Common
{
    public class BaseLib
    {

        public static int Convert_Int(string value)
        {//to check string value is null or empty.If true return default int value 0.

            int result = 0;


            if (!string.IsNullOrWhiteSpace(value))
                return Convert.ToInt32(value);

            return result;

        }


        public static decimal Convert_Decimal(string value)
        {//to check string value is null or empty.If true return default decimal value 0.
            decimal result = 0;

            if (!string.IsNullOrWhiteSpace(value))
                return Convert.ToDecimal(value);

            return result;

        }

        public static bool IsInt(string value)
        {//to check string value is whether Integer.

            int result = 0;

            if (int.TryParse(value, out result))
                return true;
            else
                return false;

        }

        public static bool IsDecimal(string value)
        {//to check string value is whether Decimal.

            decimal result = 0;

            if (decimal.TryParse(value, out result))
                return true;
            else
                return false;

        }

    }
}
