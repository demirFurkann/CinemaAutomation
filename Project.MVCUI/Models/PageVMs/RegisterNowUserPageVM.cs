using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class RegisterNowUserPageVM
    {
        public AppUserVM AppUser { get; set; }
        public AppUserProfileVM Profile { get; set; }
    }
}