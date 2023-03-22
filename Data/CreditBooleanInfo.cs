using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Application.Data
{
    public class CreditBooleanInfo
    {
        public CreditBooleanInfo()
        {

        }
        public CreditBooleanInfo(string card_number)
        {
            Card_number = card_number;
        }
        [Key]
        public string Card_number { get; set; }

        public bool Has_taken_credit { get; set; }
    }
}
