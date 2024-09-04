using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace din_im_liau.Middlewares;
public class SwaggerOrderSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var properties = context.Type.GetProperties();
        if (!properties.Any(x => IsDefinedDatamemberAttribute(x)))
        {
            return;
        }

        var dict = new Lazy<Dictionary<string, OpenApiSchema>>();
        var nonOrderProperties = properties.Where(x => !IsDefinedDatamemberAttribute(x));
        var orderProperties = properties
        .Where(x => IsDefinedDatamemberAttribute(x))
        .OrderBy(x => x.GetCustomAttribute<DataMemberAttribute>(true)!.Order)
        .ToList();

        orderProperties.AddRange(nonOrderProperties);
        foreach (var p in orderProperties)
        {
            var current = schema.Properties.FirstOrDefault(x => x.Key.Equals(p.Name, StringComparison.OrdinalIgnoreCase));

            if (current.Key == null || current.Value == null)
                continue;

            ((ICollection<KeyValuePair<string, OpenApiSchema>>)dict.Value).Add(current);
        }
        schema.Properties = dict.Value;

    }

    private bool IsDefinedDatamemberAttribute(PropertyInfo propertyInfo)
    {
        return Attribute.IsDefined(propertyInfo, typeof(DataMemberAttribute), true);
    }
}
