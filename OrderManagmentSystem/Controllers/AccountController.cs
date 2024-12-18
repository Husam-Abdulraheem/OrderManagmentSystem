﻿using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Authentication;
using OrderManagementSystem.Data;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly TokenService _tokenService;
        private readonly IAccountService _accountService;

        public AccountController(ApplicationDbContext db, TokenService tokenService, IAccountService accountService)
        {
            _db = db;
            _tokenService = tokenService;
            _accountService = accountService;
        }



        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Login(UsersAuth user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginUser = await _accountService.Login(user);
            if (loginUser == null)
            {
                return Unauthorized(new { Message = "You don't have Account" });
            }
            return Ok(loginUser);
        }

        [Route("Register/Supplier")]
        [HttpPost]
        public async Task<IActionResult> RegisterSuppliers([FromForm] RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("One or more fields are incorrect.");
            }

            var registerResult = await _accountService.RegisterSupplier(registerDto);
            if (registerResult == null)
            {
                return BadRequest("Registration failed. Please check your input and try again.");
            }

            return Ok(registerResult);
        }



        [Route("Register/Retailer")]
        [HttpPost]
        public async Task<ActionResult> RegisterRetailers([FromForm] RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("One or more fields are incorrect.");
            }
            var register = await _accountService.RegisterRetailer(registerDto);
            if (register == null)
            {
                return BadRequest("Registration failed. Please check your input and try again.");
            }
            return Ok(register);
        }
    }
}
