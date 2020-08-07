using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreProcedures.Source.Entities
{
    public class ReservationView
    {
        public int Reservation { get; set; }
        public string Startdate { get; set; }
        public string Finaldate { get; set; }

        public ReservationView() { }

        public ReservationView(int rReservation, string rStartDate, string rFinalDate)
        {
            Reservation = rReservation;
            Startdate = rStartDate;
            Finaldate = rFinalDate;
        }
    }
}