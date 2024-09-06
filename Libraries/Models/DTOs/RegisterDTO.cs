using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using Models.DataModels;

namespace Models.DTOs;

#nullable disable warnings

public class RegisterDTO
{
    [JsonPropertyOrder(1)]
    public AccountDTO Account { get; set; }
    [JsonPropertyOrder(2)]
    public string AccessToken { get; set; }
    [JsonPropertyOrder(3)]
    public string RefreshToken { get; set; }
}
