using ListaDeTarefas.Data;
using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Exemplo : ControllerBase
    {
        private readonly DBContext _context;

        public Exemplo(DBContext context)
        {
            _context = context;
        }

        [HttpPost("criar-cookie-dinamico")]
        public IActionResult CriarCookie(Login login)
        {
            var usuarioDb = _context.Usuario
                .FirstOrDefault(u => u.Email == login.Email && u.Senha == login.Senha);

            if (usuarioDb == null)
                return Unauthorized("Usuário inexistente no banco.");

            Response.Cookies.Append("Usuario", usuarioDb.Nome,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(10),
                    HttpOnly = true,
                    Secure = true
                });

            return Ok($"Cookie criado para o usuário: {usuarioDb.Nome}");
        }

        [HttpGet("ler-cookie")]
        public IActionResult LerCookie()
        {
            var usuario = Request.Cookies["Usuario"];

            if (usuario == null)
                return NotFound("Cookie não encontrado ou expirado.");

            return Ok($"Usuário lido do Cookie: {usuario}");
        }
    }
}