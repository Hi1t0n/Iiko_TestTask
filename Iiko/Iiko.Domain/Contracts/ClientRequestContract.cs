namespace Iiko.Domain.Contracts;

/// <summary>
/// DTO на запрос данных клиента
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="Username">Имя пользователя</param>
/// <param name="SystemId">Идентификатор системы</param>
public record ClientRequestContract(long? ClientId, string? Username, Guid? SystemId);