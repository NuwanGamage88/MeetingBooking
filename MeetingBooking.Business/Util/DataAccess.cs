using System.Collections.Generic;
using MeetingBooking.Business.Model;

namespace MeetingBooking.Business.Util
{
    public static class DataAccess
    {
        public static int SeqId = 0;
        public static List<BookingModel> BookingModels = new List<BookingModel>();

        public static List<RoomModel> RoomModels = new List<RoomModel>
        {
            new RoomModel {Id = 1, Name = "Room1" ,RoomType = RoomType.None},
           // new RoomModel {Id = 2, Name = "Room2"}
        };
    }
}
