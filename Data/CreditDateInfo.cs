using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Application.Data
{
    public class CreditDateInfo
    {
        public CreditDateInfo()
        {

        }
        public CreditDateInfo(string card_number)
        {
            Card_number = card_number;
        }
        [Key]
        public string Card_number { get; set; }

        public string Credit_taken_date { get; set; }

        public string Credit_toReturn_date { get; set; }

    }

}
