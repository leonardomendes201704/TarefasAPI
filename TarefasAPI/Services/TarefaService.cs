using TarefasAPI.Models;
using TarefasAPI.Repositories;
using Microsoft.Extensions.Logging;

namespace TarefasAPI.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;
        private readonly ILogger<TarefaService> _logger;

        public TarefaService(ITarefaRepository repository, ILogger<TarefaService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Tarefa> GetAll()
        {
            _logger.LogInformation("Buscando todas as tarefas.");
            return _repository.GetAll();
        }

        public Tarefa? GetById(int id)
        {
            _logger.LogInformation("Buscando tarefa com ID: {Id}", id);
            var tarefa = _repository.GetById(id);

            if (tarefa == null)
                _logger.LogWarning("Tarefa com ID {Id} não encontrada.", id);

            return tarefa;
        }

        public IEnumerable<Tarefa> GetFiltered(StatusTarefa? status, DateTime? dataVencimento)
        {
            _logger.LogInformation("Filtrando tarefas por status: {Status}, dataVencimento: {Data}", status, dataVencimento);
            var tarefas = _repository.GetAll();

            if (status.HasValue)
                tarefas = tarefas.Where(t => t.Status == status.Value);

            if (dataVencimento.HasValue)
                tarefas = tarefas.Where(t => t.DataVencimento.HasValue && t.DataVencimento.Value.Date == dataVencimento.Value.Date);

            var resultado = tarefas.ToList();
            _logger.LogInformation("Encontradas {Quantidade} tarefas com os filtros aplicados.", resultado.Count);
            return resultado;
        }

        public void Add(Tarefa tarefa)
        {
            _logger.LogInformation("Adicionando nova tarefa: {Titulo}", tarefa.Titulo);
            _repository.Add(tarefa);
        }

        public void Update(Tarefa tarefa)
        {
            _logger.LogInformation("Atualizando tarefa com ID {Id}", tarefa.Id);
            _repository.Update(tarefa);
        }

        public void Delete(int id)
        {
            _logger.LogInformation("Excluindo tarefa com ID {Id}", id);
            _repository.Delete(id);
        }
    }
}
