using ListaDeTarefas.Data;
using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DBContext _context;

        public AuthController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            var usuarioDoBanco = _context.Usuario
                .FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);

            if (usuarioDoBanco == null)
            {
                return Unauthorized("Usuário ou senha incorretos no banco de dados");
            }

            HttpContext.Session.SetString("EmailLogin", usuarioDoBanco.Email);

            Response.Cookies.Append("Email", usuarioDoBanco.Email,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    HttpOnly = true
                }
            );

            return Ok("Bem-vindo! Login realizado com sucesso!");
        }

        [HttpGet("inicial")]
        public IActionResult Inicial()
        {
            var usuario = HttpContext.Session.GetString("EmailLogin");

            if (string.IsNullOrEmpty(usuario))
            {
                return Unauthorized("Não autenticado no sistema!");
            }

            return Ok(new { mensagem = "Usuário Logado", email = usuario });
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Email");

            return Ok("Logout realizado com sucesso!");
        }
    }
}