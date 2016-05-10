using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using SnakeGame.Scoring;

namespace SnakeGame.UserInterface
{
	/// <summary>
	/// Controls the file IO for scores.
	/// </summary>
	public class ScoreIO
	{
		/// <summary>
		/// The path to the XML file containing the scores.
		/// </summary>
		public const string DATA_PATH = "Resources/xml/highscores.xml";
		/// <summary>
		/// The path to the XML schema defining the structure of DATA_PATH.
		/// </summary>
		public const string SCHEMA_PATH = "Resources/xml/highscores.xsd";
		private ValidationEventHandler _handler = delegate(object sender, ValidationEventArgs e)
		{
		};
		private XmlDocument _doc;
		/// <summary>
		/// Initializes a new instance of the <see cref="SnakeGame.UserInterface.ScoreIO"/> class.
		/// </summary>
		public ScoreIO()
		{
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.Schemas.Add(null, SCHEMA_PATH);
			settings.ValidationType = ValidationType.Schema;
			XmlReader reader = XmlReader.Create(DATA_PATH, settings);
			_doc = new XmlDocument();
			_doc.Load(reader);
			_doc.Validate(_handler);
			List<Score> scores = new List<Score>();
			LoadScores(scores,Difficulty.Easy);
			LoadScores(scores,Difficulty.Medium);
			LoadScores(scores,Difficulty.Hard);
			Collection = new ScoreCollection(scores);
			reader.Close();
			reader.Dispose();
		}
		/// <summary>
		/// Gets the set of scores being considered by this instance.
		/// </summary>
		public ScoreCollection Collection
		{
			get;
			private set;
		}
		private Difficulty GetDifficulty(string tagName)
		{
			Difficulty res;
			switch (tagName)
			{
				case "easy":
					res = Difficulty.Easy;
					break;
				case "medium":
					res = Difficulty.Medium;
					break;
				case "hard":
					res = Difficulty.Hard;
					break;
				default:
					throw new ArgumentException("Difficulty is undefined","tagName");
			}
			return res;
		}
		private void LoadScores(List<Score> scores, Difficulty d)
		{
			XmlNodeList dScores = _doc.GetFirstElementWithTagName(GetTagName(Difficulty.Easy)).GetElementsByTagName("score");
			foreach (XmlNode node in dScores)
			{
				scores.Add(new Score(d, ((XmlElement)node).GetAttribute("player"), Convert.ToInt32(node.InnerText)));
			}
		}
		private string GetTagName(Difficulty d)
		{
			string res = null;
			switch (d)
			{
				case Difficulty.Easy:
					res = "easy";
					break;
				case Difficulty.Medium:
					res = "medium";
					break;
				case Difficulty.Hard:
					res = "hard";
					break;
				default:
					throw new ArgumentException("Difficulty is undefined","d");
			}
			return res;
		}
		/// <summary>
		/// Adds a score to both the collection and the XML document structure.
		/// </summary>
		/// <param name="score">Score.</param>
		public void AddScore(Score score)
		{
			Collection.Add(score);
			XmlElement difficultyNode =  _doc.GetFirstElementWithTagName(GetTagName(score.GameplayDifficulty));
			XmlElement entry = _doc.CreateElement("score");
			entry.SetAttribute("player",score.PlayerName);
			entry.InnerText = score.Value.ToString();
			difficultyNode.AppendChild(entry);
		}
		/// <summary>
		/// Writes the XML document to the XML file.
		/// </summary>
		public void Write()
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Async = false;
			settings.CloseOutput = true;
			settings.Indent = true;
			settings.IndentChars = "\t";
			settings.NewLineChars = "\r\n";
			settings.NewLineOnAttributes = false;
			settings.WriteEndDocumentOnClose = true;
			_doc.WriteTo(XmlWriter.Create(DATA_PATH, settings));
		}
	}
}

