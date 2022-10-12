using FluentValidation;
using MyRental.Services.Areas.Ads.Dto;

namespace MyRental.Services.Areas.Ads.Validators;

public class AdDtoInputValidator : AbstractValidator<AdDtoInput>
{
    public AdDtoInputValidator()
    {
        RuleFor(ad => ad.UserId).NotEmpty();
        
        RuleFor(ad => ad.Country).NotEmpty();
        
        RuleFor(ad => ad.City).NotEmpty();
        
        RuleFor(ad => ad.Area).NotEmpty();

        RuleFor(ad => ad.Title).Length(5, 60);
        
        RuleFor(ad => ad.Rooms).NotEmpty();
        
        RuleFor(ad => ad.Square).NotEmpty();
        
        RuleFor(ad => ad.Price).NotEmpty();
        
        RuleFor(ad => ad.Description).Length(10, 250);
    }
}