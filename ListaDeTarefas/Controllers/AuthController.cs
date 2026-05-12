using Azure;
using ListaDeTarefas.Data;
using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        [HttpPost("login")]
        public IActionResult LoginUsuario(Usuario usuario)
        {
            var resultadoUsuario = _context.Usuario.Where
                (u => u.Email.Equals(usuario.Email) && u.Senha.Equals(usuario.Senha)).ToList();

            if (resultadoUsuario.Count == 0)
            {
                return Unauthorized("Email ou senha inválidos");
            }

            HttpContext.Session.SetString("IdLogado", resultadoUsuario[0].Id.ToString());
            Response.Cookies.Append("IdLogado", resultadoUsuario[0].Id.ToString());

            return Ok("Login realizado com sucesso");
        }
    }
}