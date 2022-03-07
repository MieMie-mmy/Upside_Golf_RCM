using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ORS_RCM_DL;
using ORS_RCM_Common;

namespace ORS_RCM_BL
{
    public class Jisha_Credit_Card_Payment_BL
    {
        Jisha_Credit_Card_Payment_DL JishaCreditCardDL;

        public Jisha_Credit_Card_Payment_BL()
        {
            JishaCreditCardDL = new Jisha_Credit_Card_Payment_DL();
        }

        public void Insert(Jisha_Credit_Card_Entity jishaCreditCardInfo)
        {
            JishaCreditCardDL.Insert(jishaCreditCardInfo);
        }
    }
}
