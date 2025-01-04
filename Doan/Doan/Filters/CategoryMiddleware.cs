using Doan.Data;
using Microsoft.EntityFrameworkCore;

public class CategoryMiddleware
{
    private readonly RequestDelegate _next;

    public CategoryMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ConnectDB dbContext)
    {
        if (string.IsNullOrEmpty(context.Session.GetString("categories")))
        {
            var categories = await dbContext.categorys.ToListAsync();
            string categoriesJson = System.Text.Json.JsonSerializer.Serialize(categories);

            context.Session.SetString("categories", categoriesJson);
        }

        await _next(context);
    }
}
