using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace AssetRegister
{
	public static class AssetCollectionExtensionMethods
	{

		//public static XmlDocument ToXml(this AssetCollection atc)
		//{
		//	XmlDocument doc = new XmlDocument();
		//	XPathNavigator nav = doc.CreateNavigator();
		//	using (XmlWriter w = nav.AppendChild())
		//	{
		//		XmlSerializer ser = new XmlSerializer(typeof(AssetCollection));
		//		ser.Serialize(w, atc);
		//	}

		//	return doc;
		//}

		public static XElement ToXml(this AssetCollection o)
		{
			Type t = o.GetType();

			Type[] extraTypes = t.GetProperties()
				.Where(p => p.PropertyType.IsInterface)
				.Select(p => p.GetValue(o, null).GetType())
				.ToArray();

			DataContractSerializer serializer = new DataContractSerializer(t, extraTypes);
			StringWriter sw = new StringWriter();
			XmlTextWriter xw = new XmlTextWriter(sw);
			serializer.WriteObject(xw, o);
			return XElement.Parse(sw.ToString());
		}
	}
}