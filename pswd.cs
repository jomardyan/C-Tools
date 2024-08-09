using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public static class PasswordGenerator
{
    private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
    private const string Digits = "0123456789";
    private const string SpecialCharacters = "!@#$%^&*()_+[]{}|;:,.<>?";

    public static string GeneratePassword(int length = 12, bool includeUppercase = true, bool includeLowercase = true, bool includeDigits = true, bool includeSpecialCharacters = true)
    {
        if (length <= 0)
        {
            throw new ArgumentException("Password length must be greater than zero.", nameof(length));
        }

        var characterSet = new StringBuilder();

        if (includeUppercase)
        {
            characterSet.Append(UppercaseLetters);
        }

        if (includeLowercase)
        {
            characterSet.Append(LowercaseLetters);
        }

        if (includeDigits)
        {
            characterSet.Append(Digits);
        }

        if (includeSpecialCharacters)
        {
            characterSet.Append(SpecialCharacters);
        }

        if (characterSet.Length == 0)
        {
            throw new ArgumentException("At least one character set must be included.");
        }

        return GeneratePasswordFromCharacterSet(characterSet.ToString(), length);
    }

    private static string GeneratePasswordFromCharacterSet(string characterSet, int length)
    {
        var password = new StringBuilder(length);
        using (var rng = new RNGCryptoServiceProvider())
        {
            var data = new byte[4];
            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(data);
                uint randomNumber = BitConverter.ToUInt32(data, 0);
                password.Append(characterSet[(int)(randomNumber % (uint)characterSet.Length)]);
            }
        }
        return password.ToString();
    }

    public static (int score, string strength) GetPasswordStrength(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return (0, "Invalid password");
        }

        int score = 0;

        // Length
        if (password.Length >= 8) score++;
        if (password.Length >= 12) score++;
        if (password.Length >= 16) score++;

        // Uppercase letters
        if (Regex.IsMatch(password, @"[A-Z]")) score++;

        // Lowercase letters
        if (Regex.IsMatch(password, @"[a-z]")) score++;

        // Digits
        if (Regex.IsMatch(password, @"\d")) score++;

        // Special characters
        if (Regex.IsMatch(password, @"[\W_]")) score++;

        // Return strength based on score
        return score switch
        {
            0 => (0, "Very Weak"),
            1 => (1, "Weak"),
            2 => (2, "Fair"),
            3 => (3, "Good"),
            4 => (4, "Strong"),
            5 => (5, "Very Strong"),
            6 => (6, "Excellent"),
            _ => (score, "Unknown"),
        };
    }
}
