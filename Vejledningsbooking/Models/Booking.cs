﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vejledningsbooking.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime BookingStartDateTime { get; set; }
        public DateTime BokingEndDateTime { get; set; }
    }
}
