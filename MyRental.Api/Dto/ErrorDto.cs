using System.Text.Json;

namespace MyRental.Api.Dto;

public class ErrorDto
{
    public string ErrorMessage { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}