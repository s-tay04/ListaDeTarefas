namespace ListaDeTarefas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome {  get; set; } 
        public string Email { get; set; }
        public int Senha { get; set; }
        public virtual ICollection <Tarefa>? Tarefas { get; set; }

        public Usuario (int id, string nome, string email, int senha)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}