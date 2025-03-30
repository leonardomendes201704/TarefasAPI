namespace TarefasAPI.Models
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Erro { get; set; }
        public string? Detalhe { get; set; }
    }
}
