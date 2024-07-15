using IdentityServer4.Hosting.LocalApiAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IndentityServer.Dtos;
using MultiShop.IndentityServer.Models;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IndentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto)
        {
            var values = new ApplicationUser()
            {
                Surname = userRegisterDto.Surname,
                Email = userRegisterDto.Email,
                Name = userRegisterDto.Name,
                UserName = userRegisterDto.UserName,
            };
            var result = await _userManager.CreateAsync(values,userRegisterDto.Password);
            if (result.Succeeded)
            {
                return Ok("Kullanıcı Başarıyla Eklendi");
            }
            else {
                return Ok("Bir hata oluştu tekrar deneyiniz.");
            }

        }
    }
}
