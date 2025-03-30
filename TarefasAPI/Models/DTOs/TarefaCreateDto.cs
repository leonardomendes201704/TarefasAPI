using System.ComponentModel.DataAnnotations;

using TarefasAPI.Models;

/// <summary>
/// Dados para criação de uma tarefa.
/// </summary>
public class TarefaCreateDto
{
    /// <summary>
    /// Título da tarefa.
    /// </summary>
    [Required]
    public string Titulo { get; set; }

    /// <summary>
    /// Descrição da tarefa.
    /// </summary>
    public string? Descricao { get; set; }

    /// <summary>
    /// Data de vencimento.
    /// </summary>
    public DateTime? DataVencimento { get; set; }

    /// <summary>
    /// Status da tarefa.
    /// </summary>
    public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
}
