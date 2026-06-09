using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AstroColony.Entities;
using AstroColony.Data;

namespace AstroColony.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripulantesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TripulantesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os tripulantes a bordo da AstroColony.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            return Ok(await _context.Tripulantes.ToListAsync());
        }

        /// <summary>
        /// Cadastra um novo tripulante na base.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CadastrarTripulante(Tripulante novoTripulante)
        {
            _context.Tripulantes.Add(novoTripulante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarTodos), new { id = novoTripulante.Id }, novoTripulante);
        }

        /// <summary>
        /// Atualiza a função ou o status de saúde de um tripulante.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTripulante(int id, Tripulante tripulanteAtualizado)
        {
            if (id != tripulanteAtualizado.Id) return BadRequest("IDs não conferem.");

            var tripulanteExistente = await _context.Tripulantes.FindAsync(id);
            if (tripulanteExistente == null) return NotFound("Tripulante não encontrado.");

            tripulanteExistente.Nome = tripulanteAtualizado.Nome;
            tripulanteExistente.Funcao = tripulanteAtualizado.Funcao;
            tripulanteExistente.StatusSaude = tripulanteAtualizado.StatusSaude;

            await _context.SaveChangesAsync();
            return Ok(tripulanteExistente);
        }

        /// <summary>
        /// Remove o registro de um tripulante do sistema.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverTripulante(int id)
        {
            var tripulante = await _context.Tripulantes.FindAsync(id);
            if (tripulante == null) return NotFound("Tripulante não encontrado.");

            _context.Tripulantes.Remove(tripulante);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}