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
        English_US = 0,
        French_FR = 1
    }

    private static string[] cultureNames = new string[]
    {
        "en-US",    //English from the United States
        "fr-FR"     //French from France
    };

    private static XMLManager xml;

    private static int currentLanguage;

    private static string XMLFileName
    { get { return String.Format("Resource.{0}.xml", cultureNames[currentLanguage]); } }

    /// <summary>
    /// Use this method before doing anything. This is more or less a constructor,
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
    { return xml.GetString(identifer); }

    /// <summary>
    /// Change an item in the XML file.
    /// </summary>
    /// <param name="identifer"></param>
    /// <param name="newText"></param>
    public static void Change(string identifer, string newText)
    { xml.Change(identifer, newText); }
}