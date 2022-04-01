using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaryAPI.Context;
using SalaryAPI.Models;

namespace SalaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrediccionesController : ControllerBase
    {
        private readonly ContextoPrediccion _context;

        public PrediccionesController(ContextoPrediccion context)
        {
            _context = context;
        }

        // GET: api/Predicciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prediccion>>> GetPredicciones()
        {
            return await _context.Predicciones.ToListAsync();
        }

        // GET: api/Predicciones/siguiente
        [HttpGet("siguiente")]
        public async Task<ActionResult<IEnumerable<Prediccion>>> GetSiguientePrediccion()
        {
            var lista = new List<Prediccion>();
            var pendiente = await _context.Predicciones.FirstOrDefaultAsync(x => x.Estatus == 1);

            if (pendiente != null)
            {
                pendiente.Estatus = 2;
                _context.Entry(pendiente).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                lista.Add(pendiente);
            }

            return lista;
        }

        // GET: api/Predicciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prediccion>> GetPrediccion(int id)
        {
            var prediccion = await _context.Predicciones.FindAsync(id);

            if (prediccion == null)
            {
                return NotFound();
            }

            return prediccion;
        }

        // PUT: api/Predicciones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrediccion(int id, Prediccion prediccion)
        {
            if (id != prediccion.Id)
            {
                return BadRequest();
            }

            _context.Entry(prediccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrediccionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Predicciones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Prediccion>> PostPrediccion(Prediccion prediccion)
        {
            _context.Predicciones.Add(prediccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrediccion", new { id = prediccion.Id }, prediccion);
        }

        // DELETE: api/Predicciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prediccion>> DeletePrediccion(int id)
        {
            var prediccion = await _context.Predicciones.FindAsync(id);
            if (prediccion == null)
            {
                return NotFound();
            }

            _context.Predicciones.Remove(prediccion);
            await _context.SaveChangesAsync();

            return prediccion;
        }

        private bool PrediccionExists(int id)
        {
            return _context.Predicciones.Any(e => e.Id == id);
        }
    }
}
