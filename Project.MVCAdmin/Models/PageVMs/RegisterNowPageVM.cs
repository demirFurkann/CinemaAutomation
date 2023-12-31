﻿using Project.ENTITIES.Enums;
using Project.VM.AdminPureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCAdmin.Models.PageVMs
{
    public class RegisterNowPageVM
    {
        public AdminUserVM User { get; set; }
        public AdminProfileVM Profile { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public RegisterNowPageVM()
        {

            Roles = new List<SelectListItem>();
        }
    }
}