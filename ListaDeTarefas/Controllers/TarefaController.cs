using ListaDeTarefas.Data;
using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly DBContext _context;

        public TarefaController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastraTarefa(Tarefa tarefa)
        {
            var logado = Request.Cookies["IdLogado"];
            if (logado != null)
            {
                return Unauthorized("Realize o login para continuar.");
            }

            var id = Request.Cookies["IdLogado"];
            if (id != null)
            {
                tarefa.UsuarioId = int.Parse(id);
            }

            var usuarioExiste = _context.Usuario.Any(u => u.Id == tarefa.UsuarioId);

            if (!usuarioExiste)
            {
                return NotFound($"Erro: O usuário com ID {tarefa.UsuarioId} não existe. Cadastre o usuário primeiro!");
            }

            _context.Tarefa.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizaDatos(int id, Tarefa tarefa)
        {
            var logado = Request.Cookies["IdLogado"];
            if (logado != null)
            {
                return Unauthorized("Realize o login para continuar.");
            }

            var tarefaDoBanco = _context.Tarefa.Find(id);

            if (tarefaDoBanco == null)
                return NotFound("Tarefa não encontrada.");

            tarefaDoBanco.Status = tarefa.Status;
            tarefaDoBanco.Descricao = tarefa.Descricao;
            tarefaDoBanco.UsuarioId = tarefa.UsuarioId;

            _context.SaveChanges();

            return Ok("Atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaTarefa(int id)
        {
            var logado = Request.Cookies["IdLogado"];
            if (logado != null)
            {
                return Unauthorized("Realize o login para continuar.");
            }

            var tarefa = _context.Tarefa.Find(id);

            if (tarefa == null)
                return NotFound("Tarefa não encontrada.");

            _context.Tarefa.Remove(tarefa);
            _context.SaveChanges();

            return Ok("Tarefa deletada!");
        }

        [HttpGet("{id}")]
        public IActionResult SolicitaTarefaID(int id)
        {
            var logado = Request.Cookies["IdLogado"];
            if (logado != null)
            {
                return Unauthorized("Realize o login para continuar.");
            }

            var tarefa = _context.Tarefa
                .Include(t => t.Usuario)
                .FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada");
            }
            return Ok(tarefa);
        }
    }
}