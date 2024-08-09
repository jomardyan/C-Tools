using System;
using System.Text.RegularExpressions;

public static class EmailValidator
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
        catch (Exception)
        {
            return false;
        }
    }
}

public static class NameValidator
{
    public static bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        // Validates name with only alphabetic characters, spaces, and hyphens.
        return Regex.IsMatch(name, @"^[A-Za-z\s\-]+$");
    }
}

public static class AgeValidator
{
    public static bool IsValidAge(string age)
    {
        if (int.TryParse(age, out int result))
        {
            return result >= 0 && result <= 120; // Assuming age range from 0 to 120
        }
        return false;
    }
}

public static class AddressValidator
{
    public static bool IsValidAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return false;

        // A basic validation assuming an address has at least 10 characters and can contain numbers, letters, and common symbols.
        return address.Length >= 10;
    }
}

public static class PhoneNumberValidator
{
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Validates phone numbers that may start with + followed by 10 to 15 digits
        return Regex.IsMatch(phoneNumber, @"^\+?[0-9]{10,15}$");
    }
}

public static class CharacterValidator
{
    public static bool IsValidCharacter(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        // Validates single alphabetic character
        return Regex.IsMatch(input, @"^[A-Za-z]$");
    }
}

public static class NumberValidator
{
    public static bool IsValidNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            return false;

        // Validates numeric string (integer or decimal)
        return Regex.IsMatch(number, @"^-?\d+(\.\d+)?$");
    }
}

// Example Usage in c#:
// bool emailValid = EmailValidator.IsValidEmail("test@example.com");
// bool nameValid = NameValidator.IsValidName("John Doe");
// bool ageValid = AgeValidator.IsValidAge("30");
// bool addressValid = AddressValidator.IsValidAddress("123 Main St, Springfield, IL");
// bool phoneValid = PhoneNumberValidator.IsValidPhoneNumber("+12345678901");
// bool charValid = CharacterValidator.IsValidCharacter("A");
// bool numberValid = NumberValidator.IsValidNumber("123.45");
