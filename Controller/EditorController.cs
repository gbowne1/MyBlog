using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class EditorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}