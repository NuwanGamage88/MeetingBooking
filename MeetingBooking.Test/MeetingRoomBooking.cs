using System;
using System.Collections.Generic;
using System.Text;
using MeetingBooking.Business.BusinessResource;
using MeetingBooking.Business.Model;
using NUnit.Framework;

namespace MeetingBooking.Test
{
    [TestFixture]
    public class MeetingRoomBooking
    {
        /// <summary>
        /// add booking with valid condition
        /// </summary>
        [Test]
        public void Add_Booking()
        {
            var bookingRoom1 = new MeetingRoom1BL();
            var result = bookingRoom1.SaveBooking(new BookingModel
            {
                Id = 1,
                RoomId = 1,
                StartDateTime = Convert.ToDateTime("2021.04.11 9:00 AM"),
                EndDateTime = Convert.ToDateTime("2021.04.11 11:00 AM")
            });

            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// add booking with invalid data
        /// result should not be equal to 1 
        /// </summary>
        [Test]
        public void Add_Booking_With_Invalid_Data()
        {
            var bookingRoom1 = new MeetingRoom1BL();
            var result = bookingRoom1.SaveBooking(new BookingModel
            {
                Id = 1,
                RoomId = 1,
                StartDateTime = Convert.ToDateTime("2021.04.11 11:00 AM"),
                EndDateTime = Convert.ToDateTime("2021.04.11 10:30 AM")
            });

            Assert.AreNotEqual(1, result);
        }

        /// <summary>
        /// Datetime Validation
        /// start date is greater than end date
        /// result should be -1
        /// </summary>
        [Test]
        public void Booking_StartDate_EndDate_Validation()
        {

            var bookingRoom1 = new MeetingRoom1BL();
            var validResult = bookingRoom1.SaveBooking(new BookingModel
            {
                Id = 2,
                RoomId = 1,
                StartDateTime = Convert.ToDateTime("2021.04.11 10:30 AM"),
                EndDateTime = Convert.ToDateTime("2021.04.11 10:00 AM")
            });

            Assert.AreEqual(-1, validResult);
        }
        /// <summary>
        /// Start Datetime within the schedule
        /// result should be -2
        /// </summary>
        [Test]
        public void Booking_Start_Within_Schedule_Validation()
        {

            var bookingRoom1 = new MeetingRoom1BL();
            var validResult = bookingRoom1.SaveBooking(new BookingModel
            {
                Id = 3,
                RoomId = 1,
                StartDateTime = Convert.ToDateTime("2021.04.11 10:30 AM"),
                EndDateTime = Convert.ToDateTime("2021.04.11 3:00 PM")
            });

            Assert.AreEqual(-2, validResult);
        }

        /// <summary>
        /// End Datetime within the schedule
        ///  result should be -2
        /// </summary>
        [Test]
        public void Booking_End_Within_Schedule_Validation()
        {

            var bookingRoom1 = new MeetingRoom1BL();
            var validResult = bookingRoom1.SaveBooking(new BookingModel
            {
                Id = 4,
                RoomId = 1,
                StartDateTime = Convert.ToDateTime("2021.04.11 8:30 AM"),
                EndDateTime = Convert.ToDateTime("2021.04.11 11:00 AM")
            });

            Assert.AreEqual(-2, validResult);
        }

        /// <summary>
        /// StartDate and EndDate both within schedule
        /// result should be -2
        /// </summary>
        [Test]
        public void Booking_Start_End_Within_Schedule_Validation()
        {

            var bookingRoom1 = new MeetingRoom1BL();
            var validResult = bookingRoom1.SaveBooking(new BookingModel
            {
                Id = 5,
                RoomId = 1,
                StartDateTime = Convert.ToDateTime("2021.04.11 9:30 AM"),
                EndDateTime = Convert.ToDateTime("2021.04.11 10:00 AM")
            });

            Assert.AreEqual(-2, validResult);
        }

        /// <summary>
        /// StartDateTime and EndDateTime out side the scheduled booking but overlap
        ///  result should be -2
        /// </summary>
        [Test]
        public void Booking_Start_End_OutSide_Schedule_Validation()
        {

            var bookingRoom1 = new MeetingRoom1BL();
            var validResult = bookingRoom1.SaveBooking(new BookingModel
            {
                Id = 5,
                RoomId = 1,
                StartDateTime = Convert.ToDateTime("2021.04.11 8:30 AM"),
                EndDateTime = Convert.ToDateTime("2021.04.11 11:30 AM")
            });

            Assert.AreEqual(-2, validResult);
        }

        /// <summary>
        /// valid booking , StartDate and EndDate both outside the schedule. 
        /// </summary>
        [Test]
        public void Add_Booking_OutSide_Start_End()
        {
            var bookingRoom1 = new MeetingRoom1BL();
            var result = bookingRoom1.SaveBooking(new BookingModel
            {
                Id = 2,
                RoomId = 1,
                StartDateTime = Convert.ToDateTime("2021.04.11 8:00 AM"),
                EndDateTime = Convert.ToDateTime("2021.04.11 8:59 AM")
            });

            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// delete the booking
        /// </summary>
        [Test]
        public void RemoveBooking()
        {
            var bookingRoom1 = new MeetingRoom1BL();
            var result = bookingRoom1.RemoveBooking(2);

            Assert.AreEqual(1, result);
        }
    }
}
