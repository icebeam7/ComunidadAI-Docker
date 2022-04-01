using SalaryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SalaryAPI.Context
{
    public class ContextoPrediccion : DbContext
    {
        public ContextoPrediccion(DbContextOptions<ContextoPrediccion> opciones) : base(opciones)
        {

        }

        public DbSet<Prediccion> Predicciones { get; set; }
    }
}
