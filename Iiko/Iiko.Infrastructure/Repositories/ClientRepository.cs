using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iiko.Domain.Contracts;
using Iiko.Domain.Interfaces;
using Iiko.Domain.Models;
using Iiko.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Iiko.Infrastructure.Repositories;

public class ClientRepository(ApplicationDbContext context) : IClientRepository
{
    private readonly ApplicationDbContext _context = context;
    
    public async Task<Response> AddClientAsync(ClientRequestContract client)
    {
        if (client.ClientId is null || string.IsNullOrWhiteSpace(client.Username) || client.SystemId is null)
        {
            return new Response(false, "Пользователь не добавлен. Введите корректные данные");
        }

        var entity = new Client
        {
            ClientId = client.ClientId,
            Username = client.Username,
            SystemId = client.SystemId
        };

        await _context.Clients.AddAsync(entity);
        await _context.SaveChangesAsync();

        return new Response(true, "Пользователь добавлен");
    }

    public async Task<IEnumerable<ClientResponseContract>?> AddUniqueClientAsync(IEnumerable<ClientRequestContract> clients)
    {
        if (clients.ToList().Count < 10)
        {
            return null;
        }
        
        var existingIds = await _context.Clients
            .Select(x => x.ClientId)
            .ToListAsync();

        var uniqueClients = clients.Where(c => !existingIds.Contains(c.ClientId))
            .Select(c => new Client
            {
                ClientId = c.ClientId,
                Username = c.Username,
                SystemId = c.SystemId
            })
            .ToList();
        
        var duplicateClients = clients.Where(c => existingIds.Contains(c.ClientId))
            .Select(c=> new ClientResponseContract(c.ClientId, c.Username, c.SystemId))
            .ToList();

        await _context.Clients.AddRangeAsync(uniqueClients);
        await _context.SaveChangesAsync();
        return duplicateClients;
    }

    public async Task<ClientResponseContract?> GetClientByIdAsync(long? id)
    {
        var client = await _context.Clients
            .Where(c => c.ClientId == id)
            .Select(c => new ClientResponseContract(c.ClientId, c.Username, c.SystemId))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        
        if (client is null)
        {
            return null;
        }

        return client;
    }

    public async Task<IEnumerable<ClientResponseContract>> GetAllClientsAsync()
    {
        var clients = await _context.Clients
            .Select(c=> new ClientResponseContract(c.ClientId, c.Username, c.SystemId))
            .AsNoTracking()
            .ToListAsync();
        return clients;
    }

    public async Task<Response> UpdateClientByIdAsync(long id,ClientRequestUpdateContract client)
    {
        var existingClient = await _context.Clients
            .FirstOrDefaultAsync(c => c.ClientId == id);
        
        if (existingClient is null)
        {
            return new Response(false, $"Пользователь с Id: {id} не найден");
        }

        existingClient.Username = client.Username;
        existingClient.SystemId = client.SystemId;

        _context.Clients.Update(existingClient);
        await _context.SaveChangesAsync();

        return new Response(true, $"Пользователь с Id: {id} успешно обновлён");
    }

    public async Task<Response> DeleteClientByIdAsync(long id)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.ClientId == id);

        if (client is null)
        {
            return new Response(false,$"Пользователь с Id: {id} не найден");
        }
        
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return new Response(true, $"Пользователь с Id: {client.ClientId} успешно удалён");
    }
}