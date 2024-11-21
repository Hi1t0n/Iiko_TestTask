using Iiko.Domain.Contracts;
using Iiko.Domain.Models;

namespace Iiko.Domain.Interfaces;

/// <summary>
///  Интерфейс репозитория клиента
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Добавление клиента
    /// </summary>
    /// <param name="client">DTO с данными клиента для запроса <see cref="ClientRequestContract"/>/></param>
    /// <returns>Результат добавления клиента и сообщение <see cref="Response"/></returns>
    Task<Response> AddClientAsync(ClientRequestContract client); 
    /// <summary>
    /// Добавление уникальных клиентов
    /// </summary>
    /// <param name="clients">Коллекция с данными клиентов <see cref="ClientRequestContract"/></param>
    /// <returns>Коллекция не добавленных клиентов <see cref="ClientResponseContract"/></returns>
    Task<IEnumerable<ClientResponseContract>?> AddUniqueClientAsync(IEnumerable<ClientRequestContract> clients);
    /// <summary>
    /// Получение клиентов по идентификаору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Данные клиента <see cref="ClientResponseContract"/></returns>
    Task<ClientResponseContract?> GetClientByIdAsync(long? id);
    /// <summary>
    /// Получение всех клиентов
    /// </summary>
    /// <returns>Коллекция с данными клиентов <see cref="ClientResponseContract"/></returns>
    Task<IEnumerable<ClientResponseContract>> GetAllClientsAsync();
    /// <summary>
    /// Обновление данных клиента по индетификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="client">Данные для обновления <see cref="ClientRequestUpdateContract"/></param>
    /// <returns>Результат обновления клиента и сообщение <see cref="Response"/></returns>
    Task<Response> UpdateClientByIdAsync(long id, ClientRequestUpdateContract client);
    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Результат удаления клиента и сообщение <see cref="Response"/></returns>
    Task<Response> DeleteClientByIdAsync(long id);
}