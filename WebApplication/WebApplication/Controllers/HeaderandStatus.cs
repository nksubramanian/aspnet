﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class HeaderandStatus : Controller
    {

        [HttpGet("setstatus")]
        public string Func()
        {

            HttpContext.Response.StatusCode = 300;
            return "Status code has been set";


        }

        [HttpGet("setheader")]
        public string FuncTwo()
        {

            HttpContext.Response.Headers.Add("HeaderCreated", "thisone"); 
            return "Header Created is passed";


        }

        [HttpGet]
        [Route("AnotherMethod")]
        public IActionResult AnotherMethod()
        {
            string response = "Here is the response";
            HttpContext.Response.Headers.Add("secondmethod", "thisone");
            return StatusCode(405);
        }


        [HttpGet("write-body")]
        public void WriteBody()
        {
            byte[] data = Encoding.UTF8.GetBytes("output");
            Response.Body.WriteAsync(data, 0, data.Length).Wait();
        }


    }
}