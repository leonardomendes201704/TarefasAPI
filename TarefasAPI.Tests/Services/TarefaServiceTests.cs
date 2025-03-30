using Moq;
using TarefasAPI.Models;
using TarefasAPI.Repositories;
using TarefasAPI.Services;
using Xunit;
using System.Collections.Generic;

namespace TarefasAPI.Tests.Services
{
    public class TarefaServiceTests
    {
        private readonly Mock<ITarefaRepository> _repoMock;
        private readonly TarefaService _service;

        public TarefaServiceTests()
        {
            _repoMock = new Mock<ITarefaRepository>();
            _service = new TarefaService(_repoMock.Object);
        }

        [Fact]
        public void Deve_Retornar_Todas_As_Tarefas()
        {
            // Arrange
            var listaMock = new List<Tarefa>
            {
                new Tarefa { Id = 1, Titulo = "Tarefa 1", Status = StatusTarefa.Pendente },
                new Tarefa { Id = 2, Titulo = "Tarefa 2", Status = StatusTarefa.Concluida }
            };

            _repoMock.Setup(r => r.GetAll()).Returns(listaMock);

            // Act
            var resultado = _service.GetAll();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public void Deve_Adicionar_Tarefa()
        {
            // Arrange
            var tarefa = new Tarefa
            {
                Titulo = "Nova Tarefa",
                Descricao = "Descrição",
                DataVencimento = DateTime.Now,
                Status = StatusTarefa.Pendente
            };

            // Act
            _service.Add(tarefa);

            // Assert
            _repoMock.Verify(r => r.Add(It.Is<Tarefa>(t => t.Titulo == tarefa.Titulo)), Times.Once);
        }

        [Fact]
        public void Deve_Atualizar_Tarefa_Existente()
        {
            // Arrange
            var tarefaExistente = new Tarefa
            {
                Id = 1,
                Titulo = "Tarefa Original",
                Status = StatusTarefa.Pendente
            };

            _repoMock.Setup(r => r.GetById(1)).Returns(tarefaExistente);

            var novaVersao = new Tarefa
            {
                Id = 1,
                Titulo = "Tarefa Atualizada",
                Descricao = "Nova descrição",
                DataVencimento = DateTime.Today.AddDays(1),
                Status = StatusTarefa.Concluida
            };

            // Act
            _service.Update(novaVersao);

            // Assert
            _repoMock.Verify(r => r.Update(It.Is<Tarefa>(t =>
                t.Titulo == novaVersao.Titulo &&
                t.Status == novaVersao.Status
            )), Times.Once);
        }

        [Fact]
        public void Deve_Filtrar_Tarefas_Por_Status()
        {
            // Arrange
            var lista = new List<Tarefa>
    {
        new Tarefa { Id = 1, Titulo = "T1", Status = StatusTarefa.Pendente },
        new Tarefa { Id = 2, Titulo = "T2", Status = StatusTarefa.Concluida },
        new Tarefa { Id = 3, Titulo = "T3", Status = StatusTarefa.Pendente }
    };

            _repoMock.Setup(r => r.GetAll()).Returns(lista);

            // Act
            var resultado = _service.GetFiltered(StatusTarefa.Pendente, null);

            // Assert
            Assert.All(resultado, t => Assert.Equal(StatusTarefa.Pendente, t.Status));
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public void Deve_Excluir_Tarefa_Existente()
        {
            // Arrange
            var tarefa = new Tarefa { Id = 10, Titulo = "Para excluir" };
            _repoMock.Setup(r => r.GetById(10)).Returns(tarefa);

            // Act
            _service.Delete(10);

            // Assert
            _repoMock.Verify(r => r.Delete(10), Times.Once);
        }


    }
}
