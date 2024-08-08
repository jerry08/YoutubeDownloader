using System;
using System.Text;

namespace YoutubeDownloader.Core.Utils;

public class FileEx
{
    /// <summary>
    /// Checks if thumbnail's data is valid Latin1 encoding (ISO-8859-1)
    /// </summary>
    /// <param name="input"></param>
    public static bool IsValidISO(string input) =>
        string.Equals(input, Encoding.Latin1.GetString(Encoding.Latin1.GetBytes(input)));

    /// <summary>
    /// Checks if thumbnail's data is valid Latin1 encoding (ISO-8859-1)
    /// </summary>
    /// <param name="input"></param>
    public static bool IsValidISO(byte[] input) => IsValidISO(BitConverter.ToString(input));
}
