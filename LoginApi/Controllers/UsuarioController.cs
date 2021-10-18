using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginApi.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace LoginApi.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class UsuarioController : Controller
    {
        private readonly LoginDBContext _db;
        private readonly IConfiguration _config;
        public UsuarioController(LoginDBContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        [HttpPost]
        public IActionResult Post(UserLogin usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var usuarioLogin = _db.UserLogins.Where(x => x.Usuario == usuario.Usuario).FirstOrDefault();

            if (usuarioLogin == null)
            {
                return NotFound();
            }

            if (usuarioLogin.Password == usuario.Password)
            {
                var secretKey = _config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Usuario));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                string token = tokenHandler.WriteToken(createdToken);

                return Ok(token);
            }
            else
            {
                return Forbid();
            }
        }
        [Authorize]
        public IActionResult Get()
        {
            var result = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return Ok(result.Value);
        }
    }
}
