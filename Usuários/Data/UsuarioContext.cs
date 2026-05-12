using Microsoft.EntityFrameworkCore;
using Usuários.Models;

namespace Usuários.Data
{

    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }
        public DbSet<Pessoa> Pessoas {  get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

    }




}













