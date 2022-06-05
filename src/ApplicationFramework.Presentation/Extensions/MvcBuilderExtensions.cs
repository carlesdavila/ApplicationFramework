using ApplicationFramework.Presentation.Filters;
using FluentValidation.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder WithApplicationFrameworkConfiguration(this IMvcBuilder mvcBuilder )
    {
        return mvcBuilder.AddMvcOptions(options => options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation(fv =>
            {
                fv.AutomaticValidationEnabled = false;
                fv.DisableDataAnnotationsValidation = true;
            });
    }
}