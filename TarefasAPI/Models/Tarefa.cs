﻿namespace TarefasAPI.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
    }
}