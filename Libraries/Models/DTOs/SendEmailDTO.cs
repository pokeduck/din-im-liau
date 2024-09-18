using System.Text.Json.Serialization;

namespace Models.DTOs;

#nullable disable warnings

public class SendEmailDTO
{
    [JsonPropertyOrder(1)]
    public string token { get; set; }

}
