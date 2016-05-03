using System;
using System.Xml;

namespace SnakeGame.UserInterface
{
	public static class XmlDocumentExtensions
	{
		public static XmlElement GetFirstElementWithTagName(this XmlDocument doc,string tagName)
		{
			XmlNodeList nodes = doc.GetElementsByTagName(tagName);
			return nodes.Count != 0 ? (XmlElement)nodes[0] : null;
		}
	}
}

