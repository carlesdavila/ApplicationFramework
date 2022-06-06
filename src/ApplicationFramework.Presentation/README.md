# Application Framework Presentation

![ApplicationFramework](../../logo.png)

This package is part of the Service Application Framework that consists of the following components:

* AppFramework.Domain
* AppFramework.Application
* AppFramework.Infrastructure
* **AppFramework.Presentation**

## Error Handling

Application Framework will provide a MVC Filter to handle errors.

````csharp
services.AddControllers().WithApplicationFrameworkConfiguration();
````

## Api Conventions

You can apply the ApplicationFramework [ApiConventions](https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/conventions?view=aspnetcore-6.0) to all controllers in the current assembly in the `Program.cs` with:

```csharp
using ApplicationFramework.Presentation.Web;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiConventionType(typeof(ApiConventions))]

var builder = WebApplication.CreateBuilder(args);

```
---

<sub>[Hexagon Cog](https://thenounproject.com/icon/hexagon-cog-955835/) by [Tresnatiq](https://thenounproject.com/tresnatiq/) from [the Noun Project](https://thenounproject.com/) </sub>