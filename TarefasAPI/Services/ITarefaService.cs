using System.Collections.Generic;
using TarefasAPI.Models;

namespace TarefasAPI.Services
{
    public interface ITarefaService
    {
        IEnumerable<Tarefa> GetAll();
        IEnumerable<Tarefa> GetFiltered(StatusTarefa? status, DateTime? dataVencimento);
        Tarefa? GetById(int id);
        void Add(Tarefa tarefa);
        void Update(Tarefa tarefa);
        void Delete(int id);
    }
}
