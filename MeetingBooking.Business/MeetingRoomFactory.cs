using System;
using System.Collections.Generic;
using System.Text;
using MeetingBooking.Business.BusinessResource;
using MeetingBooking.Business.Util;

namespace MeetingBooking.Business
{
    public static class MeetingRoomFactory
    {
        /// <summary>
        /// Get the specific instance base on the Meeting Room type
        /// </summary>
        /// <param name="roomType"></param>
        /// <returns></returns>
        public static IMeetingBookingBL GetInstance(RoomType roomType)
        {
            IMeetingBookingBL meetingBookingBl= null;
            if(roomType == RoomType.None)
                meetingBookingBl = new MeetingRoom1BL();
            else if (roomType == RoomType.Special)
                meetingBookingBl = new MeetingRoom1BL();

            return meetingBookingBl;
        }
    }
}
