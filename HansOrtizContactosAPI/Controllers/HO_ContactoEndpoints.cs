using Microsoft.EntityFrameworkCore;
using HansOrtizContactosAPI.Data;
using HansOrtizContactosAPI.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace HansOrtizContactosAPI.Controllers;

public static class HO_ContactoEndpoints
{
    public static void MapHO_ContactoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/HO_Contacto").WithTags(nameof(HO_Contacto));

        group.MapGet("/", async (HansOrtizContactosContextContext db) =>
        {
            return await db.HoContactos.ToListAsync();
        })
        .WithName("GetAllHO_Contactos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<HO_Contacto>, NotFound>> (int idho_contactos, HansOrtizContactosContextContext db) =>
        {
            return await db.HoContactos.AsNoTracking()
                .FirstOrDefaultAsync(model => model.IdHO_Contactos == idho_contactos)
                is HO_Contacto model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetHO_ContactoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int idho_contactos, HO_Contacto hO_Contacto, HansOrtizContactosContextContext db) =>
        {
            var affected = await db.HoContactos
                .Where(model => model.IdHO_Contactos == idho_contactos)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.IdHO_Contactos, hO_Contacto.IdHO_Contactos)
                    .SetProperty(m => m.FirstName, hO_Contacto.FirstName)
                    .SetProperty(m => m.LastName, hO_Contacto.LastName)
                    .SetProperty(m => m.PhoneNumber, hO_Contacto.PhoneNumber)
                    .SetProperty(m => m.Email, hO_Contacto.Email)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateHO_Contacto")
        .WithOpenApi();

        group.MapPost("/", async (HO_Contacto hO_Contacto, HansOrtizContactosContextContext db) =>
        {
            db.HoContactos.Add(hO_Contacto);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/HO_Contacto/{hO_Contacto.IdHO_Contactos}",hO_Contacto);
        })
        .WithName("CreateHO_Contacto")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int idho_contactos, HansOrtizContactosContextContext db) =>
        {
            var affected = await db.HoContactos
                .Where(model => model.IdHO_Contactos == idho_contactos)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteHO_Contacto")
        .WithOpenApi();
    }
}
