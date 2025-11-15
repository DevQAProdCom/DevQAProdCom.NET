using System.Xml;
using Microsoft.Extensions.Configuration.Xml;

namespace DevQAProdCom.NET.Configurations.OperativeClasses
{
    public class CustomXmlConfigurationProvider : XmlConfigurationProvider
    {
        public CustomXmlConfigurationProvider(XmlConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);

            var root = xmlDoc.DocumentElement;
            if (root != null)
            {
                LoadXmlElement(root, parentPath: null);
            }
        }

        private void LoadXmlElement(XmlElement element, string? parentPath)
        {
            var childElementGroups = element.ChildNodes
                .OfType<XmlElement>()
                .GroupBy(e => e.Name)
                .ToList();

            foreach (var group in childElementGroups)
            {
                if (group.Count() > 1)
                {
                    // Handle as array
                    int index = 0;
                    foreach (var childElement in group)
                    {
                        var key = parentPath == null ? $"{childElement.Name}:{index}" : $"{parentPath}:{childElement.Name}:{index}";
                        if (childElement.HasChildNodes && childElement.FirstChild is XmlElement)
                        {
                            LoadXmlElement(childElement, key);
                        }
                        else
                        {
                            if (childElement.GetAttribute("xsi:nil") == "true")
                            {
                                Data[key] = null;
                            }
                            else
                            {
                                Data[key] = childElement.InnerXml;
                            }
                        }
                        index++;
                    }
                }
                else
                {
                    // Handle as single element
                    var childElement = group.First();
                    var key = parentPath == null ? childElement.Name : $"{parentPath}:{childElement.Name}";

                    if (childElement.HasChildNodes && childElement.FirstChild is XmlElement)
                    {
                        LoadXmlElement(childElement, key);
                    }
                    else
                    {
                        if (childElement.GetAttribute("xsi:nil") == "true")
                        {
                            Data[key] = null;
                        }
                        else
                        {
                            Data[key] = childElement.InnerXml;
                        }
                    }
                }
            }
        }
    }
}
