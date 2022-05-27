using AuthAPI.Models;
using AuthAPI.Repositories;
using AuthAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos." });

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new


            {
                user = user,
                token = token,
            };
        }
    
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";
        
        [HttpGet]
        [Route("authenticated")]
        [AllowAnonymous]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [AllowAnonymous]
        public string Employee() => "Funcionário";
        
        [HttpGet]
        [Route("manager")]
        [AllowAnonymous]
        public string Manager() => "Gerente";
    }









}
