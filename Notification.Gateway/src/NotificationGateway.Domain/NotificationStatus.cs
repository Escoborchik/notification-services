namespace NotificationGateway.Domain;

public enum NotificationStatus
{
    Pending = 0,  // создано и сохранено (ещё не опубликовано)
    Queued = 1,   // успешно поставлено в брокер
    Failed = 2    // ошибка постановки/валидации/маршрутизации
}
