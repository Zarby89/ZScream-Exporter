/*
 * Author:  Trovsky
 */
using System;

public class ConstantsReader
{
    private static string[] cultureNames = new string[]
    {
        "JP",
        "US",
        "GR",
        "FR",
        "EU",
        "CA"
    };

    private static XMLManager xml;
    private static int currentRegion;
    private static string folderLocation;

    private static string XMLFileName
    { get { return String.Format("Constants.{0}.xml", cultureNames[currentRegion]); } }

    public const int nullAddress = -1;

    /// <summary>
    /// Use this method before doing anything. This is more or less a constructor,
    /// but you can't have constructors in a static class.
    /// </summary>
    /// <param name="region"></param>
    /// <param name="folderLocation"></param>
    public static void SetupRegion(RegionId.Region region, string folderLocation)
    {
        ConstantsReader.folderLocation = folderLocation;
        if (currentRegion != (int)RegionId.Region.Invalid)
        {
            currentRegion = (int)region;
            xml = new XMLManager(folderLocation + XMLFileName);
        }
        else throw new Exception();
    }

    /// <summary>
    /// Get an address from the XML file with the associated ID.
    /// </summary>
    /// <param name="identifer"></param>
    /// <returns></returns>
    public static int GetAddress(string identifer)
    {
        const string prefix = "0x";
        string value = xml.GetString(identifer).Replace(prefix, "");

        //Check US XML if the value is null in the other region's XML
        if (value == "" && currentRegion != (int)RegionId.Region.USA)
        {
            int tempRegion = currentRegion;
            currentRegion = (int)RegionId.Region.USA;
            value = new XMLManager(folderLocation + XMLFileName).GetString(identifer).Replace(prefix, "");
            currentRegion = tempRegion;
        }
        return ((value == "") ? nullAddress : Convert.ToInt32(value, 16));
    }
}