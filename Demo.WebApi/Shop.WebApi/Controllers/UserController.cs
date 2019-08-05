using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.Users;
using Core.Entities;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Shop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        private readonly IHostingEnvironment _env;

        public UserController(IUserService service, ILogger<UserController> logger, IHostingEnvironment env)
        {
            this._service = service;
            this._logger = logger;
            this._env = env;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequest requestObject)
        {
            try
            {
                // Validate the user in repository by username and password
                User usr = await _service.ValidateUserAsync(requestObject);
                if (usr == null)
                    return Unauthorized(new ErrorResult() { StatusCode = (int)StatusCodes.Status401Unauthorized, Message = "Wrong UserName or Password!", Error = null});

                // peocess jwt

                // save jwt into repository

                return Ok();
            }
            catch (Exception ex)
            {
                if(_env.IsDevelopment())
                    return StatusCode((int)StatusCodes.Status500InternalServerError, new ErrorResult() { StatusCode = (int)StatusCodes.Status500InternalServerError, Message = ex.Message, Error = JsonConvert.SerializeObject(ex, settings: new JsonSerializerSettings() { Formatting = Formatting.Indented, MaxDepth = 2 }) });

                // Log the exception
                _logger.LogError(message: $"Exception accured in UserController:login. {ex.Message}", exception: ex);

                return new StatusCodeResult((int)StatusCodes.Status500InternalServerError);
            }
        }

    }
}