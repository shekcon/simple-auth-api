using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.API.Validation
{
    /// <sumary> 
    /// Add ValidateModel on controller then it must check ModelState is valid or return BadRequest  
    /// </sumary>
    public class ValidateModelAttribute : ActionFilterAttribute {
    public override void OnActionExecuting(ActionExecutingContext context) {
        if (!context.ModelState.IsValid) {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
}
