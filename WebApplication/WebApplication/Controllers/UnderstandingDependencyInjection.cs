using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnderstandingDependencyInjection : Controller
    {
        private ObjectOne x;
        private ObjectTwo y;
        public UnderstandingDependencyInjection(ObjectOne x, ObjectTwo y)
        {
            this.x = x;
            this.y = y;

        }


        [HttpGet("GetHeaderData")]
        public string Func()
        {
            int a = x.GetHashCode();
            string object_one = a.ToString();
        

            int b = y.GetHashCode();
            string object_two = b.ToString();



            int c = y.ob.GetHashCode();
            string object_one_of_object_two = c.ToString();
            HttpContext.Response.StatusCode = 300;
            return "object1 is " + object_one + " object2 " + object_two + " object one of object two " + object_one_of_object_two;


        }


    }


    public class ObjectOne
    {

    }


    public class ObjectTwo
    {
        public ObjectOne ob;

        public ObjectTwo(ObjectOne ob)
        {
            this.ob = ob;
        }


    }


}

