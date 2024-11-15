using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ValidatorSpes
{
    // Validator for Norwegian Alphabet with max length of 155 characters
    public static List<bool> ValidateNorwegianAlphabet(params string[] inputs)
{
    var norwegianAlphabetRegex = new Regex("^[a-zA-ZæøåÆØÅ]{1,155}$");
    var results = new List<bool>();

    foreach (var input in inputs)
    {
        // Check if the input is null or empty, add false if it is, otherwise validate
        results.Add(!string.IsNullOrEmpty(input) && norwegianAlphabetRegex.IsMatch(input));
    }

    return results;
}


    // Validator for Address (Norwegian Alphabet + Numbers + Space) with max length of 255 characters
    public static List<bool> IsAddress(params string[] inputs)
    {
        var addressRegEx = new Regex("^[a-zA-ZæøåÆØÅ0-9\\s]{1,255}$"); // Fixed range syntax
        var results = new List<bool>();

        foreach (var input in inputs)
        {
            results.Add(addressRegEx.IsMatch(input));
        }

        return results;
    }

    // Validator for Only Numbers with max length of 8 digits
    public static List<bool> IsOnlyNumber(params string[] inputs)
    {
        var numberRegEx = new Regex("^[0-9]{1,8}$");
        var results = new List<bool>();

        foreach (var input in inputs)
        {
            results.Add(numberRegEx.IsMatch(input));
        }

        return results;
    }

    // Validator for Email with a max length of 100 characters
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

    // Validator for Phone Number with max length of 17 digits
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
}
