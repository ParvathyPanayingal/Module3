using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy1.Utilities
{
    internal class GetBookingId
    {
        [JsonProperty("bookingid")]
        public string? BookingId { get; set; }
    }
}