﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using KBriones.Models;
using Microsoft.Owin.Host.SystemWeb;

namespace KBriones.Controllers
{
    public class DataController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/forall")]
        public IHttpActionResult Get()
        {
            return Ok("Servidor date: " + DateTime.Now.ToString());
        }

        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;

            return Ok("Hello " + identity.Name);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        }


    }
}
