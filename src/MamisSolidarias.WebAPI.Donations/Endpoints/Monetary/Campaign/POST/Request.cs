using FastEndpoints;
using FluentValidation;
using MamisSolidarias.Infrastructure.Donations.Models;
using MamisSolidarias.Messages;

namespace MamisSolidarias.WebAPI.Donations.Endpoints.Monetary.Campaign.POST;

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
    /// Id of the participant, if the donation has a participant attached
    /// </summary>
    public int? ParticipantId { get; set; }
    
    /// <summary>
    /// Id of the campaign
    /// </summary>
    public int CampaignId { get; set; }
    
    /// <summary>
    /// Campaign name
    /// </summary>
    public Campaigns Campaign { get; set; }
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
        
        RuleFor(t => t.ParticipantId)
            .GreaterThan(0).When(t => t.ParticipantId is not null);
        
        RuleFor(t => t.CampaignId).GreaterThan(0);
    }
}