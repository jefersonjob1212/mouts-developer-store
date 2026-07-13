using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain;

public class IndividualClient : Client
{
    public string Cpf { get; set; }
    public string Name { get; set; }
    public DateTime BornDate { get; set; }
    public GenderIndividualClient Gender { get; set; }
}