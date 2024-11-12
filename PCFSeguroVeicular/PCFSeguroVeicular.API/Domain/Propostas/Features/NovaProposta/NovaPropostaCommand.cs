namespace PCFSeguroVeicular.API.Domain.Propostas.Features.NovaProposta;

public record NovaPropostaCommand(
    //Veículo: marca, modelo e ano
    DadosVeiculo veiculo,
    //Proprietário: cpf, nome, data nascimento, residência
    DadosCliente proprietario,
    //Condutor: cpf, data nascimento, residência.
    DadosCliente condutor,
    //Coberturas: coberturas(roubo/furto, colisão, terceiros, proteção residencial) selecionadas pelo usuário
    DadosCobertura[] coberturas
);

public record DadosVeiculo(
    string Marca,
    string Modelo,
    string Ano
);

public record DadosResidencia(
    string UF,
    string Cidade,
    string Bairro
);

public record DadosCliente(
    string Cpf,
    DateTime DtNascimento,
    DadosResidencia residencia,
    string? Nome = null
);

public record DadosCobertura(
    string Codigo
);
