namespace AstroColony.Entities
{
    public class ItemEstoque
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int QuantidadeAtual { get; set; }
        public double ConsumoMedioDiario { get; set; }

        // Chave Estrangeira (Foreign Key) para o relacionamento 1:N
        public int? ResponsavelId { get; set; }
        public Tripulante? Responsavel { get; set; }
    }
}