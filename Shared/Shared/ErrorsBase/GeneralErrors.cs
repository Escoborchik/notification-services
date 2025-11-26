namespace SharedKernel.ErrorsBase;

public static class GeneralErrors
{
    public static Error ValueIsInvalid(string? name)
    {
        string label = name ?? "значение";
        return Error.Validation("value.is.invalid", $"{label} невалидно");
    }

    public static Error NotFound(Guid? id, string? name)
    {
        string forId = id == null ? string.Empty : $" по Id '{id}'";
        return Error.NotFound("record.not.found", $"{name ?? "запись"} {forId} не удалось найти");
    }

    public static Error ValueIsRequired(string? name)
    {
        string label = name ?? string.Empty;
        return Error.Validation("length.is.invalid", $"поле {label} обязательно");
    }

    public static Error AlreadyExist()
    {
        return Error.Validation("record.already.exist", "запись уже существует");
    }

    public static Error Failure(string? message = null)
    {
        return Error.Failure("server.failure", message ?? "серверная ошибка");
    }

    public static class Court
    {
        public static Error NotFound(Guid? id = null)
        {
            return GeneralErrors.NotFound(id, "корт");
        }
    }

    public static class Organization
    {
        public static Error NotFound(Guid? id)
        {
            return GeneralErrors.NotFound(id, "организация");
        }
    }

    public static class City
    {
        public static Error NotFound(Guid? id)
        {
            return GeneralErrors.NotFound(id, "город");
        }
    }

    public static class Auth
    {
        public static Error InvalidCredentials()
        {
            return Error.Validation("credentials.is.invalid", "Неверные учетные данные");
        }

        public static Error InvalidRole()
        {
            return Error.Failure("role.is.invalid", "Неверная роль пользователя");
        }

        public static Error InvalidToken()
        {
            return Error.Validation("token.is.invalid", "Ваш токен недействителен");
        }

        public static Error ExpiredToken()
        {
            return Error.Validation("token.is.expired", "Ваш токен истек");
        }
    }

    public static class User
    {
        public static Error UserNameAlreadyExist()
        {
            return Error.Validation("username.already.exists", "Пользователь с таким именем пользователя уже существует.");
        }

        public static Error NotFound(Guid? id)
        {
            return GeneralErrors.NotFound(id, "пользователь");
        }
    }

    public static class Token
    {
        public static Error Expired()
        {
            return Error.Validation("token.is.expired", $"token is expired");
        }

        public static Error Invalid()
        {
            return Error.Validation("token.is.invalid", $"token is invalid");
        }
    }
}
