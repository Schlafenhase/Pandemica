using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreProcedures.Source.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Finaldate { get; set; }

        public Reservation() { }

        public Reservation(int rReservationId, DateTime rStartDate, DateTime rFinalDate)
        {
            ReservationId = rReservationId;
            Startdate = rStartDate;
            Finaldate = rFinalDate;
        }
    }
}