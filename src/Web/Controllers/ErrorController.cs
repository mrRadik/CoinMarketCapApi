﻿using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET
        public ActionResult Error(StatusViewModel status)
        {
            return
            View(status);
        }
    }
}