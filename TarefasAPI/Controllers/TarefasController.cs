using Microsoft.AspNetCore.Mvc;
using TarefasAPI.Models;
using TarefasAPI.Services;

namespace TarefasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _service;

        public TarefasController(ITarefaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna todas as tarefas cadastradas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Tarefa>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var tarefas = _service.GetAll();
            return Ok(tarefas);
        }

        /// <summary>
        /// Retorna tarefas filtradas por status e/ou data de vencimento.
        /// </summary>
        /// <param name="status">Status da tarefa (Pendente, EmProgresso, Concluida).</param>
        /// <param name="dataVencimento">Data de vencimento da tarefa.</param>
        [HttpGet("GetFiltered")]
        [ProducesResponseType(typeof(IEnumerable<Tarefa>), StatusCodes.Status200OK)]
        public IActionResult GetFiltered([FromQuery] StatusTarefa? status, [FromQuery] DateTime? dataVencimento)
        {
            var tarefas = _service.GetFiltered(status, dataVencimento);
            return Ok(tarefas);
        }

        /// <summary>
        /// Retorna uma tarefa específica pelo ID.
        /// </summary>
        /// <param name="id">ID da tarefa.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Tarefa), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var tarefa = _service.GetById(id);
            if (tarefa == null) return NotFound();
            return Ok(tarefa);
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        /// <param name="model">Dados da nova tarefa.</param>
        [HttpPost]
        [ProducesResponseType(typeof(Tarefa), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] TarefaCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novaTarefa = new Tarefa
            {
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                DataVencimento = model.DataVencimento,
                Status = model.Status
            };

            _service.Add(novaTarefa);

            return CreatedAtAction(nameof(GetById), new { id = novaTarefa.Id }, novaTarefa);
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <param name="id">ID da tarefa a ser atualizada.</param>
        /// <param name="tarefa">Objeto com os novos dados da tarefa.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] Tarefa tarefa)
        {
            var existente = _service.GetById(id);
            if (existente == null) return NotFound();

            tarefa.Id = id;
            _service.Update(tarefa);
            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa existente.
        /// </summary>
        /// <param name="id">ID da tarefa a ser excluída.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var existente = _service.GetById(id);
            if (existente == null) return NotFound();

            _service.Delete(id);
            return NoContent();
        }
    }
}
