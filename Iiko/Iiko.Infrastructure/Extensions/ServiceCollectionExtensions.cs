using Iiko.Domain.Interfaces;
using Iiko.Infrastructure.Context;
using Iiko.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Iiko.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавление бизнес-логики
    /// </summary>
    /// <param name="serviceCollection">Класс для расширения <see cref="IServiceCollection"/></param>
    /// <param name="connectionString">Строка подключения к бд</param>
    /// <returns>Модифицированная коллекция <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddService();
        serviceCollection.AddDataBase(connectionString);
        return serviceCollection;
    }
    
    /// <summary>
    /// Добавление базы данных
    /// </summary>
    /// <param name="serviceCollection">Класс для расширения <see cref="IServiceCollection"/></param>
    /// <param name="connectionString">Строка подключения к БД</param>
    /// <returns>Модифицированная коллекция <see cref="IServiceCollection"</returns>
    private static IServiceCollection AddDataBase(this IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(builder => builder.UseNpgsql(connectionString));
        return serviceCollection;
    }
    
    /// <summary>
    /// Добавление сервисов в DI
    /// </summary>
    /// <param name="serviceCollection">Класс для расширения <see cref="IServiceCollection"/></param>
    /// <returns>Модифицированная коллекция <see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IClientRepository, ClientRepository>();
        return serviceCollection;
    }
}