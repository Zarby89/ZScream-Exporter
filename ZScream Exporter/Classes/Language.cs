/*
 * Author:  Trovsky
 * Comment: Reference = https://msdn.microsoft.com/en-us/library/ee825488%28v=cs.20%29.aspx
 *          This class will have to be adjusted so each project has it's own 
 *          XML language file, but I'll include it here for now. Currently it references
 *          the resource folder.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows.Forms;


public static class Language
{
    public static string
        YOURPATH = "PATH";

    public static string resourceFilepath
    {
        get { return YOURPATH + "Resource." + currentLanguage + ".resx"; }
    }

    private static string[] cultureNames = new string[]
    {
        "en-US",
        "de-DE",
        "ja-JP",
        "es-MX"
    };

    private static string[] fullNames = new string[]
    {
        "English",
        "Deutsch",
        "日本語",
        "Español"
    };

    public static string[] languageName { get { return fullNames; } }

    private static string currentLanguage = cultureNames[0];

    public static string getString(string s)
    {
        ResourceManager resManager = new ResourceManager("Zeldix.resources.Resource", typeof(Form).Assembly);
        CultureInfo culture = CultureInfo.CreateSpecificCulture(currentLanguage);
        Thread.CurrentThread.CurrentUICulture =
            Thread.CurrentThread.CurrentCulture = culture;
        return resManager.GetString(s);
    }

    private static void check(Form f)
    {
        if (f.Name == "form_template")
            throw new Exception(getString("error_initialize_compo"));
    }

    public static string getString(Form f, string s)
    {
        check(f);
        return getString(f.Name + "_" + s);
    }

    public static string getStringTip(Form f, string s)
    {
        check(f);
        return getString(f.Name + "_tip_" + s);
    }

    public static string getStringError(string s)
    { return getString("error_" + s); }


    public static void setLanguage(string s)
    {
        int index = -1;
        for (int i = 0; i < fullNames.Length; i++)
            if (fullNames[i].Equals(s))
            {
                index = i;
                break;
            }
        if (index == -1)
            throw new Exception("Not a language.");
        currentLanguage = cultureNames[index];
    }

    public static void setLanguage(int i)
    { currentLanguage = cultureNames[i]; }

    //source: https://stackoverflow.com/questions/482729/c-sharp-iterating-through-an-enum-indexing-a-system-array
    private static string[] getAllLanguage()
    { return fullNames; }

    public static void AddOrUpdateResource(Form f, string key, string value)
    { AddOrUpdateResource(getString(f, key), value); }

    public static void AddOrUpdateResource(string key, string value)
    {
        var resx = new List<DictionaryEntry>();
        using (var reader = new ResXResourceReader(resourceFilepath))
        {
            resx = reader.Cast<DictionaryEntry>().ToList();
            var existingResource = resx.Where(r => r.Key.ToString() == key).FirstOrDefault();
            if (existingResource.Key == null && existingResource.Value == null) // NEW!
                resx.Add(new DictionaryEntry() { Key = key, Value = value });
            else // MODIFIED RESOURCE!
            {
                var modifiedResx = new DictionaryEntry()
                { Key = existingResource.Key, Value = value };
                resx.Remove(existingResource);  // REMOVING RESOURCE!
                resx.Add(modifiedResx);  // AND THEN ADDING RESOURCE!
            }
        }
        using (var writer = new ResXResourceWriter(resourceFilepath))
        {
            resx.ForEach(r =>
            {
                // Again Adding all resource to generate with final items
                writer.AddResource(r.Key.ToString(), r.Value.ToString());
            });
            writer.Generate();
        }


        //added code
        /*
         * This updates ResourceManager's version of the data
         */
        //resManager = new ResourceManager("Zeldix.resources.Resource", typeof(form_template).Assembly);
    }

}
