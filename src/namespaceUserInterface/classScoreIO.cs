using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using SnakeGame.Scoring;

namespace SnakeGame.UserInterface
{
	public class ScoreIO
	{
		public const string DATA_PATH = "Resources/xml/highscores.xml";
		public const string SCHEMA_PATH = "Resources/xml/highscores.xsd";
		private ValidationEventHandler _handler = delegate(object sender, ValidationEventArgs e)
		{
		};
		private XmlDocument _doc;
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
		}
		public void LoadScores(List<Score> scores, Difficulty d)
		{
			XmlNodeList dScores = _doc.GetFirstElementWithTagName(GetTagName(Difficulty.Easy)).GetElementsByTagName("score");
			foreach (XmlNode node in dScores)
			{
				scores.Add(new Score(d, ((XmlElement)node).GetAttribute("player"), Convert.ToInt32(node.InnerText)));
			}
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
		public ScoreCollection Collection
		{
			get;
			private set;
		}
		public void AddScore(Score score)
		{
			Collection.Add(score);
			XmlElement difficultyNode =  _doc.GetFirstElementWithTagName(GetTagName(score.GameplayDifficulty));
			XmlElement entry = _doc.CreateElement("score");
			entry.SetAttribute("player",score.PlayerName);
			entry.InnerText = score.Value.ToString();
			difficultyNode.AppendChild(entry);
		}
		public void Write()
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Async = true;
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

