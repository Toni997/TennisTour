namespace TennisTour.Application.Models.Validators.User;

public static class UserValidatorConfiguration
{
    public const int MinimumUsernameLength = 5;

    public const int MaximumUsernameLength = 20;

    public const int MinimumPasswordLength = 6;

    public const int MaximumPasswordLength = 128;

    public const int MinYearFromNowBirth = 100;

    public const int MaxYearFromNowBirth = 18;

    public const int MinHeight = 100;

    public const int MaxHeight = 300;

    public const int MinWeight = 10;

    public const int MaxWeight = 200;
}
