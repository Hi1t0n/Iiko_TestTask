namespace Iiko.Domain.Contracts;

/// <summary>
/// DTO для обновления клиента
/// </summary>
/// <param name="Username">Имя клиента</param>
/// <param name="SystemId">Идентификатор системы</param>
public record ClientRequestUpdateContract(string Username, Guid SystemId);