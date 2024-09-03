using System.ComponentModel;
using System.Runtime.Serialization;

namespace Models.Responses;

#nullable disable warnings

public class BaseResponse
{


    [DataMember(Order = 1)]
    public virtual int Code { get; set; } = 200;

    [DataMember(Order = 2)]
    public virtual string Message { get; set; }

}

public class GenericResponse<T> : BaseResponse
{

    [DataMember(Order = 3)]
    public virtual T Data { get; set; }
}

public class DefaultResponse : BaseResponse
{

    [DataMember(Order = 3)]
    [DefaultValue(null)]
    public virtual object? Data { get; set; }
}