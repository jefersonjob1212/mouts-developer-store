using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Clients.SearchClients;

/// <summary>
/// Response model for SearchClientsResult operation
/// </summary>
public class SearchClientsResult
{
    /// <summary>
    /// The unique identifier of the client
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The client phone number
    /// </summary>
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// The client email
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// The client address
    /// </summary>
    public string Address { get; set; }
    
    /// <summary>
    /// The client city
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// The client state
    /// </summary>
    public string State { get; set; }
    
    /// <summary>
    /// The individual client CPF
    /// </summary>
    public string? Cpf { get; set; }
    
    /// <summary>
    /// The individual client name
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// The individual client born date
    /// </summary>
    public DateTime? BornDate { get; set; }
    
    /// <summary>
    /// The individual client gender
    /// </summary>
    public GenderIndividualClient? Gender { get; set; }
    
    /// <summary>
    /// The company client CNPJ
    /// </summary>
    public string? Cnpj { get; set; }
    
    /// <summary>
    /// The company client legal name
    /// </summary>
    public string? LegalName { get; set; }
    
    /// <summary>
    /// The company client trade name
    /// </summary>
    public string? TradeName { get; set; }
}