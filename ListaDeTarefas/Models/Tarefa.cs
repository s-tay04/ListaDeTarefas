namespace ListaDeTarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }

        public Tarefa(int id, string status, string descricao, int usuarioId)
        {
            this.Id = id;
            this.Status = status;
            this.Descricao = descricao;
            this.UsuarioId = usuarioId;
        }
    }
}