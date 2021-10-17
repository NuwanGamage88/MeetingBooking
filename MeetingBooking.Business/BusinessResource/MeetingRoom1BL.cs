using System;
using System.Collections.Generic;
using System.Text;

namespace MeetingBooking.Business.BusinessResource
{
    public class MeetingRoom1BL : MeetingBooking
    {
        public MeetingRoom1BL()
        {

        }
        /// <summary>
        /// Can implement Room1 Specific validation.
        /// </summary>
        /// <returns></returns>
        public override int SpecificValidation()
        {

            return 1;
        }
    }
}
