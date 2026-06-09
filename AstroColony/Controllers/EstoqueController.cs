using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AstroColony.Entities;
using AstroColony.Data;

namespace AstroColony.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstoqueController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os itens de suprimento cadastrados na base.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            return Ok(await _context.ItensEstoque.ToListAsync());
        }

        /// <summary>
        /// Cadastra um novo item no estoque de suprimentos.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarItem(ItemEstoque novoItem)
        {
            _context.ItensEstoque.Add(novoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarTodos), new { id = novoItem.Id }, novoItem);
        }

        /// <summary>
        /// Atualiza os dados de um item de estoque existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarItem(int id, ItemEstoque itemAtualizado)
        {
            if (id != itemAtualizado.Id) return BadRequest("IDs não conferem.");

            var itemExistente = await _context.ItensEstoque.FindAsync(id);
            if (itemExistente == null) return NotFound("Item não encontrado.");

            itemExistente.Nome = itemAtualizado.Nome;
            itemExistente.QuantidadeAtual = itemAtualizado.QuantidadeAtual;
            itemExistente.ConsumoMedioDiario = itemAtualizado.ConsumoMedioDiario;

            await _context.SaveChangesAsync();
            return Ok(itemExistente);
        }

        /// <summary>
        /// Remove um item do controle de estoque.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarItem(int id)
        {
            var item = await _context.ItensEstoque.FindAsync(id);
            if (item == null) return NotFound("Item não encontrado.");

            _context.ItensEstoque.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Calcula a previsão de término de um item e alerta se há risco de ruptura.
        /// </summary>
        [HttpGet("{id}/previsao-termino")]
        public async Task<IActionResult> CalcularPrevisao(int id)
        {
            var item = await _context.ItensEstoque.FindAsync(id);
            if (item == null) return NotFound("Item não encontrado.");
            if (item.ConsumoMedioDiario <= 0) return BadRequest("O consumo deve ser maior que zero.");

            double diasRestantes = item.QuantidadeAtual / item.ConsumoMedioDiario;
            string statusRisco = diasRestantes <= 5 ? "CRÍTICO - Risco de Ruptura!" : "Seguro";

            return Ok(new
            {
                Item = item.Nome,
                QuantidadeEmEstoque = item.QuantidadeAtual,
                ConsumoPorDia = item.ConsumoMedioDiario,
                DiasDeCobertura = Math.Round(diasRestantes, 1),
                Status = statusRisco
            });
        }
    }
}