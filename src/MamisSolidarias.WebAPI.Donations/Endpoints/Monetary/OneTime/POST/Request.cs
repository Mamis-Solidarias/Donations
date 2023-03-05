using FastEndpoints;
using FluentValidation;
using MamisSolidarias.Messages;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.OneTime.POST;

public class Request
{
    /// <summary>
    /// Id of the donor, if one exists
    /// </summary>
    public int? DonorId { get; set; }
    
    /// <summary>
    /// Amount of money donated
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Currency of the donation
    /// </summary>
    public Currency Currency { get; set; }
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0);
        
        RuleFor(x => x.Currency).IsInEnum();
        
        RuleFor(x => x.DonorId)
            .GreaterThan(0)
            .When(x => x.DonorId.HasValue);
    }
}