using System;
using System.Collections.Generic;
using BackendWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace BackendWebApi.Models;

public partial class Osto
{
    public int Id { get; set; }

    public string Tuote { get; set; } = null!;

    public decimal Hinta { get; set; }

    public DateOnly Päivä { get; set; }

    public string Kauppa { get; set; } = null!;

    public string? Osoite { get; set; }

    public string? Tietoja { get; set; }
}


public static class OstoEndpoints
{
	public static void MapOstoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Osto").WithTags(nameof(Osto));

        group.MapGet("/", async (OstoksetContext db) =>
        {
            return await db.Ostos.ToListAsync();
        })
        .WithName("GetAllOstos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Osto>, NotFound>> (int id, OstoksetContext db) =>
        {
            return await db.Ostos.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Osto model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetOstoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Osto osto, OstoksetContext db) =>
        {
            var affected = await db.Ostos
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, osto.Id)
                  .SetProperty(m => m.Tuote, osto.Tuote)
                  .SetProperty(m => m.Hinta, osto.Hinta)
                  .SetProperty(m => m.Päivä, osto.Päivä)
                  .SetProperty(m => m.Kauppa, osto.Kauppa)
                  .SetProperty(m => m.Osoite, osto.Osoite)
                  .SetProperty(m => m.Tietoja, osto.Tietoja)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateOsto")
        .WithOpenApi();

        group.MapPost("/", async (Osto osto, OstoksetContext db) =>
        {
            db.Ostos.Add(osto);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Osto/{osto.Id}",osto);
        })
        .WithName("CreateOsto")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OstoksetContext db) =>
        {
            var affected = await db.Ostos
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteOsto")
        .WithOpenApi();
    }
}