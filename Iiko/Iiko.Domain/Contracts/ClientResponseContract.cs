namespace Iiko.Domain.Contracts;

/// <summary>
/// DTO для возвращения данных
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="Username">Имя пользователя</param>
/// <param name="SystemId">Идентификатор системы</param>
public record ClientResponseContract(long? ClientId, string? Username, Guid? SystemId);