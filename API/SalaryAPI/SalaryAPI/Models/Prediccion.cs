using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalaryAPI.Models
{
    [Table("Predictions")]
    public class Prediccion
    {
        [Key]
        public int Id { get; set; }

        public int Estatus { get; set; }
        public double TiempoExperiencia { get; set; }
        public double Salario { get; set; }
    }
}
