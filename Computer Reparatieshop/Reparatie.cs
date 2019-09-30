using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Reparatieshop
{
    class Reparatie
    {
        public string Opdrachtgever { get; set; }
        public string Opdracht { get; set; }
        public TimeSpan Deadline { get; set; }
        public StatusEnum Status { get; set; }
        public string Reparateur { get; set; }
        public int Resterendetijd
        {
            get
            {
                var resterendetijd = (Deadline.Day - DateTime.Now.Day).TotalDays;
               /* if (Deadline.AddDays(resterendetijd) >= DateTime.Now)
                    resterendetijd--;
                    */
                return resterendetijd;                 
            }
        }
    }
    enum StatusEnum
    {
        Verzoek, Ontvangen, Uitvoering, Klaar
    }
}

