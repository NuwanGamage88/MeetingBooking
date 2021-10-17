using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeetingBooking.Business.Model
{
    public class BookingModel
    {
        public int Id { get; set; }
        [DisplayName("Room")]
        [Required]
        public int RoomId { get; set; }

        public string  RoomName { get; set; }

        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        [Required]
        public DateTime StartDateTime { get; set; }
        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        [Required]
        public DateTime EndDateTime { get; set; }
    }
}
