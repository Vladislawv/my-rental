namespace MyRental.Api.Dto;

public class ErrorDto
{
    public string ErrorMessage { get; set; }
    public object? AdditionalInfo { get; set; }
}