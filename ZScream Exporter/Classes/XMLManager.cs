/*
 * Author:  Trovsky
 */

using System;
using System.IO;
using System.Text;
using System.Xml;

/// <summary>
/// Read and writes to an XML file
/// </summary>
public class XMLManager
{
    private string path;
    private XmlDocument doc;
    private const string input = "//*[@id='{0}']";

    public XMLManager(string path)
    {
        this.path = path;
        doc = new XmlDocument();
        using (StreamReader oReader = new StreamReader(path, Encoding.GetEncoding("ISO-8859-1")))
            doc.Load(oReader);
    }

    public string GetString(string identifer)
    {
        if (path != string.Empty)
            return (doc.SelectSingleNode(string.Format(input, identifer))).InnerText;
        else throw new NullReferenceException();
    }

    public void Change(string identifer, string newText)
    {
        XmlNode node = doc.SelectSingleNode((string.Format(input, identifer)));
        node.InnerText = newText;
        doc.Save(path);
    }
}