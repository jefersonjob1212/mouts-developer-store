namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryById;

/// <summary>
/// Response model for GetSubsidiaryByIdResult operation
/// </summary>
public class GetSubsidiaryByIdResult
{
    /// <summary>
    /// Unique ID of subsidiary
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Unique CNPJ of subsidiary
    /// </summary>
    public string Cnpj { get; set; }
    
    /// <summary>
    /// Legal name of subsidiary
    /// </summary>
    public string LegalName { get; set; }
    
    /// <summary>
    /// Trade name of subsidiary
    /// </summary>
    public string TradeName { get; set; }
    
    /// <summary>
    /// Address of subsidiary
    /// </summary>
    public string Address { get; set; }
    
    /// <summary>
    /// City of subsidiary
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// State of subsidiary
    /// </summary>
    public string State { get; set; }
}