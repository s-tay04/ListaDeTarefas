using ListaDeTarefas.Models;
using ListaDeTarefas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DBContext _context;

        public UsuarioController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastraUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
            return Created("", usuario);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizaDatos(int id, Usuario usuario)
        {
            var usuarioDoBanco = _context.Usuario.Find(id);

            if (usuarioDoBanco == null)
                return NotFound("Usuário não encontrado.");

            usuarioDoBanco.Nome = usuario.Nome;
            usuarioDoBanco.Email = usuario.Email;
            usuarioDoBanco.Senha = usuario.Senha;

            _context.SaveChanges();

            return Ok("Atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaUsuario(int id)
        {
            var usuario = _context.Usuario.Find(id);

            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();

            return Ok("Usuário deletado!");
        }

        [HttpGet("{id}")]
        public IActionResult SolicitaUsuarioID(int id)
        {
            var usuario = _context.Usuario
                .Include(u => u.Tarefas)
                .FirstOrDefault(u => u.Id == id);

            if (usuario == null) return NotFound("Usuário não encontrado");

            return Ok(usuario);
        }
    }
}