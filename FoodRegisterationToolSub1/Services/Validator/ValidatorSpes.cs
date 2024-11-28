using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FoodRegisterationToolSub1.Models.users;

public class ValidatorSpes
{
  
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



     public static bool IsValidUserType(UserType userType)
    {
        
        return Enum.IsDefined(typeof(UserType), userType);
    }

    
}
