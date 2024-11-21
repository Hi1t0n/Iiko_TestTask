namespace Iiko.Domain.Contracts;

/// <summary>
/// Ответ из репозиториев
/// </summary>
/// <param name="Flag">Флаг успеха действия</param>
/// <param name="Message">Сообщение</param>
public record Response(bool Flag,string Message);