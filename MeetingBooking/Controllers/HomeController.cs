using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MeetingBooking.Business;
using MeetingBooking.Business.BusinessResource;
using MeetingBooking.Business.Model;
using MeetingBooking.Business.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MeetingBooking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeetingBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  IMeetingBookingBL _meetingBookingBl;
        private readonly List<RoomModel> _roomModels;
        public HomeController(ILogger<HomeController> logger, IMeetingBookingBL metMeetingBookingBl)
        {
            _meetingBookingBl = metMeetingBookingBl;
            _logger = logger;
            _roomModels = _meetingBookingBl.GetMeetingRooms();

             _logger.LogInformation("Home Controller logger");
        }

        public IActionResult Index()
        {
            try
            {
                var bookingList = _meetingBookingBl.GetBookingModels();
                return View(bookingList);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in fetching booking process. " + ex);
                return RedirectToAction("Error");
            }
           
        }
        [HttpGet]
        public IActionResult Booking()
        {
            TempData["RoomList"] = ToSelectList(_roomModels);
            return View();
        }

       
        public IActionResult Delete(int id)
        {
            try
            {
                _meetingBookingBl.RemoveBooking(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError("Error deleting data." + ex);
                return RedirectToAction("Error");
            }
          
        }

        [HttpPost]
        public IActionResult Booking(BookingModel bookingModel)
        {
            try
            {
                var result = _meetingBookingBl.SaveBooking(bookingModel);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }

                switch (result)
                {
                    case -1:
                    {
                        TempData["RoomList"] =  ToSelectList(_roomModels);
                        ModelState.AddModelError("", "StartDateTime and EndDateTime is not valid");
                        return View("Booking");
                    }
                    case -2:
                    {
                        TempData["RoomList"] = ToSelectList(_roomModels);
                        ModelState.AddModelError("", "Already had booking in selected time range");
                        return View("Booking");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["RoomList"] = ToSelectList(_roomModels);
                ModelState.AddModelError("", "Error saving data! Please try again.");
                _logger.LogError("Error saving data." + ex);
                return View("Booking");
            }

            return RedirectToAction("Error");
        }


        [NonAction]
        public SelectList ToSelectList(List<RoomModel> roomModels)
        {
            var list = roomModels.Select(row => new SelectListItem() {Text = row.Name, Value = row.Id.ToString()}).ToList();
            return new SelectList(list, "Value", "Text");
        }

        [HttpPost]
        public IActionResult ChangeRoom(string roomId)
        {
            //TODO- base on the room id can get the RoomType
            var roomTypeInstance = MeetingRoomFactory.GetInstance(RoomType.None);
            _meetingBookingBl = roomTypeInstance;
            return View("Booking");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
