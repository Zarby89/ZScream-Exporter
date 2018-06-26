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
        XmlNode n = (doc.SelectSingleNode(string.Format(input, identifer)));
        return (n == null ? "" : n.InnerText);
    }

    public void Change(string identifer, string newText)
    {
        XmlNode node = doc.SelectSingleNode((string.Format(input, identifer)));
        node.InnerText = newText;
        doc.Save(path);
    }
}