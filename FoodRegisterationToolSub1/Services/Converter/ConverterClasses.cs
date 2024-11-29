using System;
using System.IO;
/// <summary>
/// Provides methods for converting images to Base64-encoded strings.
/// </summary>
public class Base64Converter
{
    /// <summary>
    /// Converts an image file to its Base64 string representation.
    /// </summary>
    /// <param name="imagePath">The full path to the image file to be converted.</param>
    /// <returns>A Base64-encoded string representing the image.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="imagePath"/> is null or empty.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the specified file does not exist.</exception>
    /// <exception cref="IOException">Thrown if an I/O error occurs while reading the file.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if access to the file is denied.</exception>
    public static string ConvertImageToBase64(string imagePath)
    {
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        return Convert.ToBase64String(imageBytes);
    }
}
