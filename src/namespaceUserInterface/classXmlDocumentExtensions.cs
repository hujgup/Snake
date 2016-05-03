using System;
using System.Xml;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Provides extension methods for XmlDocument.
	/// </summary>
	public static class XmlDocumentExtensions
	{
		/// <summary>
		/// Gets the first element with a given tag name.
		/// </summary>
		/// <param name="doc">The document to search in.</param>
		/// <param name="tagName">The tag name to search for.</param>
		public static XmlElement GetFirstElementWithTagName(this XmlDocument doc,string tagName)
		{
			XmlNodeList nodes = doc.GetElementsByTagName(tagName);
			return nodes.Count != 0 ? (XmlElement)nodes[0] : null;
		}
	}
}

