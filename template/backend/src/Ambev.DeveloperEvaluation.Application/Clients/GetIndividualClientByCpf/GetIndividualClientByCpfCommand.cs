using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByCpf;

/// <summary>
/// Command for retrieving a company client by CPF
/// </summary>
public class GetIndividualClientByCpfCommand : IRequest<GetIndividualClientByCpfResult>
{
    /// <summary>
    /// The unique CPF of the company client to retrieve
    /// </summary>
    public string Cpf { get; set; }

    /// <summary>
    /// Initialize a new instance of GetIndividualClientByCpfCommand
    /// </summary>
    /// <param name="cpf"></param>
    public GetIndividualClientByCpfCommand(string cpf)
    {
        Cpf = cpf;
    }
}