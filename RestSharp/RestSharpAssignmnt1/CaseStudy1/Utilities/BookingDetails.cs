using CaseStudy1.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy1.Utilities
{
    internal class BookingDetails
    {
        [JsonProperty("bookingid")]
        public string? BookingId { get; set; }

        [JsonProperty("firstname")]
        public string? firstname { get; set; }

        [JsonProperty("lastname")]
        public string? LastName { get; set; }
        [JsonProperty("totalprice")]
        public string? TotalPrice { get; set; }

        [JsonProperty("depositpaid")]
        public string? DepositPaid { get; set; }
        [JsonProperty("bookingdates")]
        public BookingDate BookingDate { get; set; }

        [JsonProperty("additionalneeds")]
        public string? AdditionalNeeds { get; set; }



    }
}