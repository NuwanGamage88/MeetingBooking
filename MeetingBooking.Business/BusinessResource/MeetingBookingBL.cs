using System;
using MeetingBooking.Business.Model;
using System.Collections.Generic;
using System.Linq;
using MeetingBooking.Business.Util;

namespace MeetingBooking.Business.BusinessResource
{
    public abstract  class MeetingBooking : IMeetingBookingBL
    {
        /// <summary>
        /// Save the booking 
        /// </summary>
        /// <param name="bookingItem"></param>
        /// <returns>int</returns>
        public int SaveBooking(BookingModel bookingItem)
        {
            try
            {
               
                var validResult = ValidBooking(bookingItem);
                if (validResult <= 0) return validResult;

                bookingItem.Id = ++DataAccess.SeqId;
                DataAccess.BookingModels.Add(bookingItem);
                return validResult;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Remove the booking base on the id
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns>int</returns>

        public int RemoveBooking(int bookingId)
        {
            try
            {
                var deletedItem = DataAccess.BookingModels.FirstOrDefault(x => x.Id == bookingId);
                if (deletedItem != null)
                {
                    DataAccess.BookingModels.Remove(deletedItem);
                    return 1;
                }
                return 0;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Booking validation
        /// </summary>
        /// <param name="bookingItem"></param>
        /// <returns> -1-> StartDateTime is greater than EndDateTime
        ///  -2 -> Already has some booking within selected range
        /// 1 -> booking is valid 
        /// </returns>
        public int ValidBooking(BookingModel bookingItem)
        {
            if (bookingItem.StartDateTime >= bookingItem.EndDateTime)
                return -1;

            if (DataAccess.BookingModels.Any() && DataAccess.BookingModels.Any(x => x.RoomId == bookingItem.RoomId
                && ((bookingItem.StartDateTime >= x.StartDateTime && bookingItem.StartDateTime <= x.EndDateTime)
                    || (bookingItem.EndDateTime >= x.StartDateTime && bookingItem.EndDateTime <= x.EndDateTime)
                    || (bookingItem.StartDateTime <= x.StartDateTime && x.EndDateTime <= bookingItem.EndDateTime))))
                return -2;

            return 1;
        }

     /// <summary>
     /// Get All the meeting booking
     /// </summary>
     /// <returns></returns>
        public List<BookingModel> GetBookingModels()
        {
            if (DataAccess.BookingModels == null) return new List<BookingModel>();

            var bookingList = DataAccess.BookingModels;
            var roomList = DataAccess.RoomModels;
            var result = from b in bookingList
                join r in roomList on b.RoomId equals r.Id 
                select new BookingModel
                {
                    Id = b.Id,
                    RoomId = r.Id,
                    RoomName = r.Name,
                    StartDateTime = b.StartDateTime,
                    EndDateTime = b.EndDateTime
                };

            return result.ToList();

        }
        /// <summary>
        /// Get all the meeting rooms
        /// </summary>
        /// <returns></returns>
        public List<RoomModel> GetMeetingRooms()
        {
           return DataAccess.RoomModels;
        }

        /// <summary>
        /// if there are any specific validation base on the meeting room can implement.
        /// </summary>
        /// <returns></returns>
        public abstract int SpecificValidation();
    }
}
