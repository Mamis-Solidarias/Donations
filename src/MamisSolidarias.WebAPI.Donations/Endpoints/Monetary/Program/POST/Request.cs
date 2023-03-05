using FastEndpoints;
using FluentValidation;
using MamisSolidarias.Messages;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.Program.POST;

public sealed class Request
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

    /// <summary>
    /// Program of the donation
    /// </summary>
    public string Program { get; set; } = string.Empty;
}

internal sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(t => t.DonorId)
            .GreaterThan(0).When(t => t.DonorId is not null);

        RuleFor(t => t.Amount)
            .GreaterThan(0);

        RuleFor(t => t.Currency).IsInEnum();

        RuleFor(t => t.Program).NotEmpty();
    }
}