using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FoodRegisterationToolSub1.Models.users;

public class ValidatorSpes
{
  /// <summary>
    /// Validates if the input strings contain only characters from the Norwegian alphabet (lowercase and uppercase).
    /// </summary>
    /// <param name="inputs">The strings to validate.</param>
    /// <returns>A list of boolean values indicating if each input is valid according to the Norwegian alphabet regex.</returns>
    public static List<bool> ValidateNorwegianAlphabet(params string[] inputs)
{
    var norwegianAlphabetRegex = new Regex("^[a-zA-ZæøåÆØÅ]{1,155}$");
    var results = new List<bool>();

    foreach (var input in inputs)
    {
        results.Add(!string.IsNullOrEmpty(input) && norwegianAlphabetRegex.IsMatch(input));
    }

    return results;
}

    /// <summary>
    /// Validates if the input strings are valid addresses containing alphanumeric characters and Norwegian special characters.
    /// </summary>
    /// <param name="inputs">The strings to validate as addresses.</param>
    /// <returns>A list of boolean values indicating if each input matches the address regex pattern.</returns>
    public static List<bool> IsAddress(params string[] inputs)
    {
        var addressRegEx = new Regex("^[a-zA-ZæøåÆØÅ0-9\\s]{1,255}$"); 
        var results = new List<bool>();

        foreach (var input in inputs)
        {
            results.Add(addressRegEx.IsMatch(input));
        }

        return results;
    }

   /// <summary>
    /// Validates if the input strings contain only numeric characters.
    /// </summary>
    /// <param name="inputs">The strings to validate as numbers.</param>
    /// <returns>A list of boolean values indicating if each input consists only of numbers.</returns>
    public static List<bool> IsOnlyNumber(params string[] inputs)
    {
        var numberRegEx = new Regex("^[0-9]{1,8}$");
        var results = new List<bool>();

        foreach (var input in inputs)
        {
            results.Add(!string.IsNullOrEmpty(input) && numberRegEx.IsMatch(input));
        }

        return results;
    }

  /// <summary>
    /// Validates if the input strings are valid email addresses.
    /// </summary>
    /// <param name="inputs">The strings to validate as email addresses.</param>
    /// <returns>A list of boolean values indicating if each input matches the email regex pattern.</returns>
    public static List<bool> IsEmail(params string[] inputs)
    {
        var emailRegEx = new Regex("^[a-zA-Z0-9]+[a-zA-Z0-9._%+-]{0,98}@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"); // Fixed escaping for dot
        var results = new List<bool>();

        foreach (var input in inputs)
        {
            results.Add(input.Length <= 100 && emailRegEx.IsMatch(input));
        }

        return results;
    }

    /// <summary>
    /// Validates if the input strings are valid phone numbers consisting of digits.
    /// </summary>
    /// <param name="inputs">The strings to validate as phone numbers.</param>
    /// <returns>A list of boolean values indicating if each input matches the phone number regex pattern.</returns>
    public static List<bool> IsPhoneNumber(params string[] inputs)
    {
        var phoneNumberRegEx = new Regex("^[0-9]{1,17}$");
        var results = new List<bool>();

        foreach (var input in inputs)
        {
            results.Add(phoneNumberRegEx.IsMatch(input));
        }

        return results;
    }

     /// <summary>
    /// Validates if a provided user type is a valid value according to the <see cref="UserType"/> enum.
    /// </summary>
    /// <param name="userType">The user type to validate.</param>
    /// <returns>True if the user type is valid, otherwise false.</returns>

     public static bool IsValidUserType(UserType userType)
    {
        
        return Enum.IsDefined(typeof(UserType), userType);
    }

    
}
