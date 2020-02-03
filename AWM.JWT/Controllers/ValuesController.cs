﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace AWM.JWT.Controllers
{
    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //This resource is For all types of role
        //[Authorize(Roles = "SuperAdmin, Admin, User, cliente")]
        [ClaimsAuthorization(ClaimType = "TESTE", ClaimValue = "EE")]
        [HttpGet]
        [Route("api/values/getvalues")]
        
        public IHttpActionResult GetValues()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var LogTime = identity.Claims
                      .FirstOrDefault(c => c.Type == "LoggedOn").Value;
            var type = identity.Claims.GetType();

            

            return Ok("Hello: " + identity.Name + ", " +
                " Your Role(s) are: " + string.Join(",", roles.ToList()) +
                " Your Login time is :" + LogTime);
        }
    }
}
