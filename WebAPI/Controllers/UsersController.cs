﻿using Business.Repositories.UserRepository;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _userService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            var result = await _userService.GetByPhone(phone);
            if (result.Success)
            {

                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetUserInfo(string phone)
        {
            var result = await _userService.GetUserInfo(phone);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Update(string Email, string Name, string Phone, DateTime BirthDay)
        {
           
            var user = await _userService.GetUserByPhoneNumber(Phone); 
            if (user != null)
            {
                user.Name = Name;
                user.Phone = Phone;
                user.BirthDay = BirthDay;
                user.Email = Email;

                var result = await _userService.Update(user); 
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            else
            {
                return NotFound("Kullanıcı bulunamadı."); 
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(User user)
        {
            var result = await _userService.Delete(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{email}")]
        public async Task<IActionResult> SendConfirmUserMail(string email)
        {
            var result = await _userService.SendConfirmUserMail(email);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{email}")]
        public async Task<IActionResult> SendForgotPasswordMail(string email)
        {
            var result = await _userService.SendForgotPasswordMail(email);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{confirmValue}")]
        public async Task<IActionResult> ConfirmUser(string confirmValue)
        {
            var result = await _userService.ConfirmUser(confirmValue);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateANewPassword(CreateANewPasswordDto createANewPasswordDto)
        {
            var result = await _userService.CreateANewPassword(createANewPasswordDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var result = await _userService.ChangePassword(userChangePasswordDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
