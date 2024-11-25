using System.Collections.Generic;
using System.Threading.Tasks;
using Iiko.Domain.Contracts;
using Iiko.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Iiko.API.Routing;

public static class ClientRoutes
{
    public static WebApplication AddClientRouting(this WebApplication application)
    {
        var clientGroup = application.MapGroup("/api/client");
        
        clientGroup.MapPost(pattern: "/add", handler: AddClient);
        clientGroup.MapPost(pattern: "/add-unique", handler: AddUniqueClient);
        clientGroup.MapGet(pattern: "/get/", handler: GetAllClients);
        clientGroup.MapGet(pattern: "/get/{id:long}", handler: GetClientById);
        clientGroup.MapPut(pattern: "/update/{id:long}", handler: UpdateUserById);
        clientGroup.MapDelete(pattern: "delete/{id:long}", handler: DeleteClientById);
        
        return application;
    }

    public async static Task<IResult> AddClient(ClientRequestContract client, IClientRepository repository)
    {
        var response = await repository.AddClientAsync(client);

        return response.Flag ? Results.Ok(new {message = response.Message}) : Results.BadRequest(new {message = response.Message});
    }

    public async static Task<IResult> AddUniqueClient([FromBody]IEnumerable<ClientRequestContract> clients, IClientRepository repository)
    {
        var clientsDuplicates = await repository.AddUniqueClientAsync(clients);

        return clientsDuplicates is not null
            ? Results.Ok(clientsDuplicates)
            : Results.BadRequest(new {message = "Для использования этого метода необходимо минимум 10 пользователей"});
    }

    public async static Task<IResult> GetClientById(long id, IClientRepository repository)
    {
        var client = await repository.GetClientByIdAsync(id);

        return client is not null
            ? Results.Ok(client)
            : Results.NotFound(new {message = $"Пользователь с Id: {id} не найден"});
    }

    public async static Task<IResult> GetAllClients(IClientRepository repository)
    {
        var clients = await repository.GetAllClientsAsync();
        return Results.Ok(clients);
    }

    public async static Task<IResult> UpdateUserById(long id, ClientRequestUpdateContract client, IClientRepository repository)
    {
        if (string.IsNullOrWhiteSpace(client.Username) || client.SystemId == Guid.Empty)
        {
            Results.BadRequest(new { message = "Введите корректные данные" });
        }
        
        var result = await repository.UpdateClientByIdAsync(id, client);

        return result.Flag
            ? Results.Ok(result.Message)
            : Results.NotFound(new
            {
                message = result.Message
            });
    }

    public async static Task<IResult> DeleteClientById(long id, IClientRepository repository)
    {
        var result = await repository.DeleteClientByIdAsync(id);
        
        return result.Flag
            ? Results.Ok(result.Message)
            : Results.NotFound(new
            {
                message = result.Message
            });
    }
}