namespace eSlozka.Domain.Constants;

public static class Routes
{
    public const string Home = "/";

    public static class Authentication
    {
        public const string Base = "/auth";
        public const string Login = $"{Base}/login";
        public const string Logout = $"{Base}/logout";
        public const string Register = $"{Base}/register";
    }
}
