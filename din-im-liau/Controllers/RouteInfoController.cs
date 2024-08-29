// using System.Text;
// using din_im_liau.Controllers;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Abstractions;
// using Microsoft.AspNetCore.Mvc.Infrastructure;

// public class RouteInfoController : BaseController
// {
//     // for accessing conventional routes...
//     private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

//     public RouteInfoController(
//         IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
//     {
//         _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
//     }

//     public IActionResult Index()
//     {
//         StringBuilder sb = new StringBuilder();

//         foreach (ActionDescriptor ad in _actionDescriptorCollectionProvider.ActionDescriptors.Items)
//         {
//             var action = Url.Action(new Microsoft.AspNetCore.Mvc.Routing.UrlActionContext()
//             {
//                 Action = ad.RouteValues["action"],
//                 Controller = ad.RouteValues["controller"],
//                 Values = ad.RouteValues
//             });

//             sb.AppendLine(action).AppendLine().AppendLine();
//         }

//         return Ok(sb.ToString());
//     }
// }
