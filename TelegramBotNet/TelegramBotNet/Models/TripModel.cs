using System;

namespace TelegramBotNet.Models
{
    public class TripModel
    {
        public string Destination { get; set; }

        public string Organization { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}