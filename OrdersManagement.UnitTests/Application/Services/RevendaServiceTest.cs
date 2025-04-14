using Moq;
using OrdersManagement.Application.Interfaces.Repositories;
using OrdersManagement.Application.Services;
using OrdersManagement.Domain.DTOs;
using OrdersManagement.Domain.Entities;
using OrdersManagement.Domain.ValueObjects;

namespace OrdersManagement.UnitTests.Application.Services
{
    public class RevendaServiceTest
    {
        private readonly Mock<IRevendaRepository> _revendaRepositoryMock;
        private readonly RevendaService _revendaService;

        public RevendaServiceTest()
        {
            _revendaRepositoryMock = new Mock<IRevendaRepository>();
            _revendaService = new RevendaService(_revendaRepositoryMock.Object);
        }

        [Fact]
        public async Task CreatePedidoClienteAsync_ShouldThrowIfNotFound()
        {
            _revendaRepositoryMock.Setup(r => r.GetRevendaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Revenda)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _revendaService.CreatePedidoClienteAsync(1, new PedidoClienteRequestDTO()));
        }

        [Fact]
        public async Task CreatePedidoClienteAsync_ShouldCreatePedido()
        {
            var request = new PedidoClienteRequestDTO();
            _revendaRepositoryMock
                .Setup(r => r.GetRevendaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Revenda
                {
                    NomeFantasia = "Nome Fantasia",
                    Cnpj = new Cnpj("12345678000195"),
                    RazaoSocial = "Razao Social",
                    Email = "test@example.com",
                    Contatos = new List<Contato>(),
                    Enderecos = new List<Endereco>()
                });
            _revendaRepositoryMock
                .Setup(r => r.CreatePedidoClienteAsync(It.IsAny<int>(), request))
                .ReturnsAsync(new PedidoClienteResponseDTO());

            var result = await _revendaService.CreatePedidoClienteAsync(1, request);

            Assert.NotNull(result);
            _revendaRepositoryMock.Verify(r => r.CreatePedidoClienteAsync(1, request), Times.Once);
        }

        [Fact]
        public async Task CreateRevendaAsync_ShouldThrowIfRevendaExists()
        {
            var revendaDto = new RevendaDTO {
                Id = 5,
                NomeFantasia = "Nome Fantasia",
                Cnpj = new Cnpj("12345678000195"),
                RazaoSocial = "Razao Social",
                Email = "test@example.com",
                Contatos = new List<ContatoDTO>(),
                Enderecos = new List<EnderecoDTO>()
            };
            _revendaRepositoryMock.Setup(r => r.GetRevendaByIdAsync(revendaDto.Id))
                .ReturnsAsync(new Revenda
                {
                    NomeFantasia = "Nome Fantasia",
                    Cnpj = new Cnpj("12345678000195"),
                    RazaoSocial = "Razao Social",
                    Email = "test@example.com",
                    Contatos = new List<Contato>(),
                    Enderecos = new List<Endereco>()
                });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _revendaService.CreateRevendaAsync(revendaDto));
        }

        [Fact]
        public async Task CreateRevendaAsync_ShouldCreateRevenda()
        {
            var dto = new RevendaDTO
            {
                NomeFantasia = "Nome Fantasia",
                Cnpj = new Cnpj("12345678000195"),
                RazaoSocial = "Razao Social",
                Email = "test@example.com",
                Contatos = new List<ContatoDTO>(),
                Enderecos = new List<EnderecoDTO>()
            };
            _revendaRepositoryMock
                .Setup(r => r.GetRevendaByIdAsync(dto.Id))
                .ReturnsAsync((Revenda)null);
            _revendaRepositoryMock
                .Setup(r => r.CreateRevendaAsync(It.IsAny<Revenda>()))
                .ReturnsAsync(new RevendaDTO
            {
                NomeFantasia = dto.NomeFantasia,
                Cnpj = dto.Cnpj,
                RazaoSocial = dto.RazaoSocial,
                Email = dto.Email,
                Contatos = dto.Contatos,
                Enderecos = dto.Enderecos
            });

            var result = await _revendaService.CreateRevendaAsync(dto);

            Assert.Equal(dto.NomeFantasia, result.NomeFantasia);
            Assert.Equal(dto.RazaoSocial, result.RazaoSocial);
            Assert.Equal(dto.Cnpj.Value, result.Cnpj.Value);
            Assert.Equal(dto.Email, result.Email);
        }

        [Fact]
        public async Task DeleteRevendaAsync_ShouldThrowIfNotFound()
        {
            _revendaRepositoryMock.Setup(r => r.GetRevendaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Revenda)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _revendaService.DeleteRevendaAsync(1));
        }

        [Fact]
        public async Task DeleteRevendaAsync_ShouldReturnTrueIfDeleted()
        {
            _revendaRepositoryMock
                .Setup(r => r.GetRevendaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Revenda
                {
                    NomeFantasia = "Nome Fantasia",
                    Cnpj = new Cnpj("12345678000195"),
                    RazaoSocial = "Razao Social",
                    Email = "test@example.com",
                    Contatos = new List<Contato>(),
                    Enderecos = new List<Endereco>()
                });
            _revendaRepositoryMock
                .Setup(r => r.DeleteRevendaAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var result = await _revendaService.DeleteRevendaAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task GetAllRevendasAsync_ShouldThrowIfNoneFound()
        {
            _revendaRepositoryMock
                .Setup(r => r.GetAllRevendasAsync())
                .ReturnsAsync((IEnumerable<Revenda>)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _revendaService.GetAllRevendasAsync());
        }

        [Fact]
        public async Task GetAllRevendasAsync_ShouldReturnList()
        {
            var revendas = new List<Revenda> { new Revenda
            {
                    NomeFantasia = "Nome Fantasia",
                    Cnpj = new Cnpj("12345678000195"),
                    RazaoSocial = "Razao Social",
                    Email = "test@example.com",
                    Contatos = new List<Contato>(),
                    Enderecos = new List<Endereco>()
                } };
            _revendaRepositoryMock
                .Setup(r => r.GetAllRevendasAsync())
                .ReturnsAsync(revendas);

            var result = await _revendaService.GetAllRevendasAsync();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetRevendaByIdAsync_ShouldThrowIfNotFound()
        {
            _revendaRepositoryMock.Setup(r => r.GetRevendaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Revenda)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _revendaService.GetRevendaByIdAsync(1));
        }

        [Fact]
        public async Task UpdateRevendaAsync_ShouldThrowIfNotFound()
        {
            var revendaDto = new RevendaDTO
            {
                Id = 5,
                NomeFantasia = "Nome Fantasia",
                Cnpj = new Cnpj("12345678000195"),
                RazaoSocial = "Razao Social",
                Email = "test@example.com",
                Contatos = new List<ContatoDTO>(),
                Enderecos = new List<EnderecoDTO>()
            };
            _revendaRepositoryMock.Setup(r => r.GetRevendaByIdAsync(revendaDto.Id))
                .ReturnsAsync((Revenda)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _revendaService.UpdateRevendaAsync(revendaDto));
        }

        [Fact]
        public async Task UpdateRevendaAsync_ShouldUpdateRevenda()
        {
            var revendaDto = new RevendaDTO 
            {
                Id = 1,
                NomeFantasia = "Nome Fantasia",
                Cnpj = new Cnpj("12345678000195"),
                RazaoSocial = "Razao Social",
                Email = "test@example.com",
                Contatos = new List<ContatoDTO>(),
                Enderecos = new List<EnderecoDTO>()
            };
            _revendaRepositoryMock
                .Setup(r => r.GetRevendaByIdAsync(revendaDto.Id))
                .ReturnsAsync(new Revenda 
            {
                Id = 1,
                NomeFantasia = "Nome Fantasia",
                Cnpj = new Cnpj("12345678000195"),
                RazaoSocial = "Razao Social",
                Email = "test@example.com",
                Contatos = new List<Contato>(),
                Enderecos = new List<Endereco>()
            });
            _revendaRepositoryMock
                .Setup(r => r.UpdateRevendaAsync(It.IsAny<Revenda>()))
                .ReturnsAsync(revendaDto);

            var result = await _revendaService.UpdateRevendaAsync(revendaDto);

            Assert.Equal(revendaDto.Id, result.Id);
            Assert.Equal(revendaDto.NomeFantasia, result.NomeFantasia);
            Assert.Equal(revendaDto.Cnpj.Value, result.Cnpj.Value);
            Assert.Equal(revendaDto.RazaoSocial, result.RazaoSocial);
            Assert.Equal(revendaDto.Email, result.Email);
            Assert.Equal(revendaDto.Contatos.Count, result.Contatos.Count);
            Assert.Equal(revendaDto.Enderecos.Count, result.Enderecos.Count);
        }
    }
}