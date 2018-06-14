/*
 * Author:  Trovsky
 */
using System;

/// <summary>
/// Manages different translation files of an application.
/// </summary>
public static class TextAndTranslationManager
{
    //https://msdn.microsoft.com/en-us/library/ee825488%28v=cs.20%29.aspx

    /// <summary>
    /// Language enum
    /// </summary>
    public enum XLanguage
    {
        English_US = 1,
        French_FR = 2
    }

    private const int invalid_language_val = 0;

    private static string[] cultureNames = new string[]
    {
        "en-US",    //English from the United States
        "fr-FR"     //French from France
    };

    private static XMLManager xml;

    private static int currentLanguage;

    private static string XMLFileName
    { get { return String.Format("Resource.{0}.xml", cultureNames); } }

    /// <summary>
    /// Use this method before doing anything. This is more or less a constructor
    /// but you can't have constructors in a static class.
    /// </summary>
    /// <param name="language"></param>
    /// <param name="folderLocation"></param>
    public static void SetupLanguage(XLanguage language, string folderLocation)
    {
        currentLanguage = (int)language;
        xml = new XMLManager(folderLocation + XMLFileName);
    }

    /// <summary>
    /// Get a string from the XML file with the associated ID.
    /// </summary>
    /// <param name="identifer"></param>
    /// <returns></returns>
    public static string GetString(string identifer)
    {
        if (currentLanguage != invalid_language_val)
            return xml.GetString(identifer);
        else throw new Exception();
    }

    /// <summary>
    /// Change an item in the XML file.
    /// </summary>
    /// <param name="identifer"></param>
    /// <param name="newText"></param>
    public static void Change(string identifer, string newText)
    {
        if (currentLanguage != invalid_language_val)
            xml.Change(identifer, newText);
        else throw new Exception();
    }
}