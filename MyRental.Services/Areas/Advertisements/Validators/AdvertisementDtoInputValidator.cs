using FluentValidation;
using MyRental.Services.Areas.Advertisements.Dto;

namespace MyRental.Services.Areas.Advertisements.Validators;

public class AdvertisementDtoInputValidator : AbstractValidator<AdvertisementDtoInput>
{
    public AdvertisementDtoInputValidator()
    {
        RuleFor(ad => ad.UserId).NotEmpty();
        
        RuleFor(ad => ad.Country).NotEmpty();
        
        RuleFor(ad => ad.City).NotEmpty();
        
        RuleFor(ad => ad.Area).NotEmpty();

        RuleFor(ad => ad.Title).NotEmpty()
            .Length(5, 60);
        
        RuleFor(ad => ad.Rooms).NotEmpty();
        
        RuleFor(ad => ad.Square).NotEmpty();
        
        RuleFor(ad => ad.Price).NotEmpty();
        
        RuleFor(ad => ad.Description).NotEmpty()
            .Length(10, 250);
    }
}