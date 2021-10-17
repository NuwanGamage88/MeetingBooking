using System.Collections.Generic;
using MeetingBooking.Business.Util;

namespace MeetingBooking.Business.Model
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoomType RoomType { get; set; }
        public List<BookingModel> BookingList { get; set; }
    }
}
