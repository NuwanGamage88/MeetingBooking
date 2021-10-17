using System.Collections.Generic;
using MeetingBooking.Business.Model;

namespace MeetingBooking.Business.BusinessResource
{
    public interface IMeetingBookingBL
    {
        int SaveBooking(BookingModel bookingItem);
        int RemoveBooking(int bookingId);

        int ValidBooking(BookingModel bookingItem);

        int SpecificValidation();

        List<BookingModel> GetBookingModels();

        List<RoomModel> GetMeetingRooms();
    }
}
