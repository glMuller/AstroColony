using Microsoft.EntityFrameworkCore;
using AstroColony.Entities; 

namespace AstroColony.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tabela 1: Estoque
        public DbSet<ItemEstoque> ItensEstoque { get; set; }
        
        // Tabela 2: Tripulação ---> É ESTA LINHA QUE FAZ O VERMELHO SUMIR
        public DbSet<Tripulante> Tripulantes { get; set; } 
    }
}