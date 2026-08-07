using Domain.Shared.Results;
using System.Text;

namespace Domain.Auth.ValueObjects;

public record RegistrationCode
{
    private const string SafeAlphabet = "ABCDEFGHJKMNPQRSTVWXYZ23456789";
    private const int CodeLength = 12;

    private string Value { get; init; } = string.Empty;

    public static Result<RegistrationCode> Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length != CodeLength)
            return Result<RegistrationCode>.Failure(new Error("Invalid registration code format", ErrorType.Validation));

        return Result<RegistrationCode>.Success(new RegistrationCode { Value = code });
    }

    private RegistrationCode() { }

    public RegistrationCode(string? code = null)
    {
        Value = code ?? GenerateCode();
    }

    private static string GenerateCode()
    {
        var random = Random.Shared;
        var code = new StringBuilder(CodeLength);

        for (int i = 0; i < CodeLength; i++)
        {
            code.Append(SafeAlphabet[random.Next(SafeAlphabet.Length)]);
        }

        return code.ToString();
    }

    public override string ToString() => Value;
}
