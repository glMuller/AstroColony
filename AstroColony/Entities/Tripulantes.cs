using System.Text.Json.Serialization;

namespace AstroColony.Entities
{
    public class Tripulante
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;
        public string StatusSaude { get; set; } = "Apto";

        // Relacionamento 1:N - Um tripulante pode ser responsável por vários itens de estoque
        [JsonIgnore]
        public ICollection<ItemEstoque> ItensSobResponsabilidade { get; set; } = new List<ItemEstoque>();
    }
}