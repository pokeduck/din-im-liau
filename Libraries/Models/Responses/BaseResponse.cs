using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.Responses;

#nullable disable warnings

public class BaseResponse
{

    [JsonPropertyOrder(order: 1)]
    [DataMember(Order = 1)]
    public virtual int Code { get; set; } = 200;

    [JsonPropertyOrder(order: 2)]
    [DataMember(Order = 2)]
    public virtual string Message { get; set; }

}

public class GenericResponse<T> : BaseResponse
{
    [JsonPropertyOrder(order: 3)]
    [DataMember(Order = 3)]
    public virtual T Data { get; set; }
}

public class DefaultResponse : BaseResponse
{
    [JsonPropertyOrder(order: 3)]
    [DataMember(Order = 3)]
    [DefaultValue(null)]
    public virtual object? Data { get; set; }
}
