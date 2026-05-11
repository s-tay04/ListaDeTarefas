using Microsoft.EntityFrameworkCore;
using ListaDeTarefas.Models;

namespace ListaDeTarefas.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
    }
}