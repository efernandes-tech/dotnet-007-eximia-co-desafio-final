using CSharpFunctionalExtensions;
using EscolaEximia.HttpService.Dominio.Entidades;
using EscolaEximia.HttpService.Dominio.Infraestrutura;
using EscolaEximia.HttpService.Handlers;
using Moq;
using System.Threading.Tasks;

namespace EscolaEximia.TestesUnidade;

public class RealizarInscricoesTestes
{
    [Fact]
    public async Task DeveAdicionarInscricaoAtivaQuandoTodosOsCriteriosSaoAtendidos()
    {
        // Arrange
        var mockRepositorio = new Mock<InscricoesRepositorio>();

        mockRepositorio.Setup(r => r.AlunoExiste(It.IsAny<string>())).ReturnsAsync(true);
        mockRepositorio.Setup(r => r.ResponsavelExiste(It.IsAny<string>())).ReturnsAsync(true);

        var turma = new Turma(
            Id: 1,
            Vagas: 10,
            Masculino: true,
            Feminino: true,
            LimiteIdade: 18
        );
        mockRepositorio.Setup(r => r.RecuperarTurma(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Maybe<Turma>.From(turma));

        var aluno = new Aluno
        {
            Sexo = ESexo.Masculino,
            Idade = 15
        };
        mockRepositorio.Setup(r => r.RecuperarAluno(It.IsAny<string>()))
            .ReturnsAsync(Maybe<Aluno>.From(aluno));

        mockRepositorio.Setup(r => r.Adicionar(It.IsAny<Inscricao>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        mockRepositorio.Setup(r => r.Save()).Returns(Task.CompletedTask);

        var handler = new RealizarInscricaoHandler(mockRepositorio.Object);

        var command = new RealizarInscricaoCommand
        {
            Aluno = "12345678900",
            Responsavel = "98765432100",
            Turma = 1
        };

        // Act
        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(resultado.IsSuccess);
        Assert.NotNull(resultado.Value);
        Assert.True(resultado.Value.Ativa);
        Assert.Equal(command.Aluno, resultado.Value.Aluno);
        Assert.Equal(command.Responsavel, resultado.Value.Responsavel);
        Assert.Equal(command.Turma, resultado.Value.Turma);

        mockRepositorio.Verify(r => r.Adicionar(It.IsAny<Inscricao>(), It.IsAny<CancellationToken>()), Times.Once);
        mockRepositorio.Verify(r => r.Save(), Times.Once);
    }
}