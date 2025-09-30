using Xunit;
using Moq;
using SisCaixaBanco.Services;
using SisCaixaBanco.Repositories;
using SisCaixaBanco.Models;
using SisCaixaBanco.DTO;
using System.Threading.Tasks;
using SisCaixaBanco.Common.Enums;

namespace SisCaixaBancoVindi.Tests
{
    public class ContaServiceTests
    {
        [Fact]
        public async Task CriarContaAsync_CriaConta()
        {
            // Arrange
            var mockRepo = new Mock<IContaRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
       
            mockUnitOfWork.Setup(u => u.Contas).Returns(mockRepo.Object);

            mockRepo.Setup(r => r.GetByDocumentoAsync("12345678900")).ReturnsAsync((Conta)null);

            var service = new ContaService(mockRepo.Object, mockUnitOfWork.Object);
            var contaDto = new ContaCreateDTO { Nome = "NomePessoaTeste", Documento = "12345678900" };

            // Act
            await service.CriarContaAsync(contaDto);

            // Assert
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Conta>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        }

        [Fact]
        public async Task ListarContasPorNomeAsync()
        {
            // Arrange
            var contas = new List<Conta>
            {
                new Conta { NomeCliente = "João", Documento = "111" },
                new Conta { NomeCliente = "Maria", Documento = "222" }
            };

            var mockRepo = new Mock<IContaRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Contas).Returns(mockRepo.Object);
            mockRepo.Setup(r => r.SearchAsync("João", null)).ReturnsAsync(contas.FindAll(c => c.NomeCliente.Contains("João")));

            var service = new ContaService(mockRepo.Object, mockUnitOfWork.Object);

            // Act
            var resultado = await service.ListarContasAsync("João", null);

            // Assert
            Assert.Single(resultado);
            Assert.Equal("João", resultado.First().NomeCliente);
        }
        [Fact]
        public async Task ListarContasPorDocumentoAsync()
        {
            // Arrange
            var contas = new List<Conta>
            {
                new Conta { NomeCliente = "João", Documento = "111" },
                new Conta { NomeCliente = "Maria", Documento = "222" }
             };

            var mockRepo = new Mock<IContaRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Contas).Returns(mockRepo.Object);
            // Corrigido: o setup deve usar os mesmos parâmetros que o método testado
            mockRepo.Setup(r => r.SearchAsync(null, "222"))
                .ReturnsAsync(contas.FindAll(c => c.Documento == "222"));

            var service = new ContaService(mockRepo.Object, mockUnitOfWork.Object);

            // Act
            var resultado = await service.ListarContasAsync(null, "222");

            // Assert
            Assert.Single(resultado);
            Assert.Equal("222", resultado.First().Documento);
        }
        [Fact]
        public async Task InativarContaPorDocumentoAsync_InativaConta()
        {
            // Arrange
            var conta = new Conta { Id = 1, NomeCliente = "Teste", Documento = "123", Status = StatusConta.Ativa };
            var mockContaRepo = new Mock<IContaRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockContaLogRepo = new Mock<IRepository<ContaLog>>();

            mockUnitOfWork.Setup(u => u.Contas).Returns(mockContaRepo.Object);
            mockUnitOfWork.Setup(u => u.ContasLog).Returns(mockContaLogRepo.Object);
            mockContaRepo.Setup(r => r.GetByDocumentoAsync("123")).ReturnsAsync(conta);

            var service = new ContaService(mockContaRepo.Object, mockUnitOfWork.Object);

            // Act
            await service.InativarContaPorDocumentoAsync("123");

            // Assert
            mockContaRepo.Verify(r => r.Update(It.Is<Conta>(c => c.Status == StatusConta.Inativa)), Times.Once);
            mockContaLogRepo.Verify(r => r.AddAsync(It.IsAny<ContaLog>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task TransferirAsync_RealizaTransferenciaEntreContas()
        {
            // Arrange
            var contaOrigem = new Conta { Id = 1, NomeCliente = "Origem", Documento = "111", Saldo = 1000, Status = StatusConta.Ativa };
            var contaDestino = new Conta { Id = 2, NomeCliente = "Destino", Documento = "222", Saldo = 500, Status = StatusConta.Ativa };
            var transferenciaDto = new TransferenciaDTO { DocumentoOrigem = "111", DocumentoDestino = "222", Valor = 200 };

            var mockContaRepo = new Mock<IContaRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTransferenciaRepo = new Mock<IRepository<Transferencia>>();

            mockUnitOfWork.Setup(u => u.Contas).Returns(mockContaRepo.Object);
            mockUnitOfWork.Setup(u => u.Transferencias).Returns(mockTransferenciaRepo.Object);
            mockContaRepo.Setup(r => r.GetByDocumentoAsync("111")).ReturnsAsync(contaOrigem);
            mockContaRepo.Setup(r => r.GetByDocumentoAsync("222")).ReturnsAsync(contaDestino);

            var service = new ContaService(mockContaRepo.Object, mockUnitOfWork.Object);

            // Act
            await service.TransferirAsync(transferenciaDto);

            // Assert
            mockContaRepo.Verify(r => r.Update(It.Is<Conta>(c => c.Id == 1 && c.Saldo == 800)), Times.Once);
            mockContaRepo.Verify(r => r.Update(It.Is<Conta>(c => c.Id == 2 && c.Saldo == 700)), Times.Once);
            mockTransferenciaRepo.Verify(r => r.AddAsync(It.IsAny<Transferencia>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

    }
}
