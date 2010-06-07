using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace ManagedAssembly.Web
{
	public static class TextExtensions
	{
		public static string Linkify(this string input) {
			string[] words = input.Split(' ');

			for (int i = 0; i < words.Length; i++) {
				if (Uri.IsWellFormedUriString(words[i], UriKind.Absolute)) {
					words[i] = string.Format("<a href=\"{0}\" rel=\"nofollow\" target=\"_blank\">{0}</a>", words[i]);
				}
			}

			return string.Join(" ", words);
		}

		public static string Codify(this string input, bool linkify) {
			if (string.IsNullOrEmpty(input))
				return input;

			bool isInCodeBlock = false;
			string[] lines = input.Split(new[] { '\n' }, StringSplitOptions.None);

			for (int i = 0; i < lines.Length; i++) {
				bool isFirst = i == 0;
				bool isLast = i == lines.Length - 1;
				bool isNotFirst = i > 0;
				bool isNotLast = i < lines.Length - 1;
				string nextLine = (isNotLast ? lines[i + 1] : "").TrimEnd();
				string prevLine = (isNotFirst ? lines[i - 1] : "").TrimEnd();
				bool nextLineIsCode = isLast ? false : nextLine.StartsWith("    ");
				bool prevLineIsCode = prevLine.StartsWith("    ");
				string prefix = "";
				string suffix = "";
				string contents = HttpUtility.HtmlEncode(lines[i].TrimEnd());
				bool thisLineIsCode = contents.StartsWith("    ");

				if (((isFirst) || (isNotFirst && !isInCodeBlock)) && thisLineIsCode) {
					prefix = "<pre>" + Environment.NewLine;
					isInCodeBlock = true;
				}

				if (!isInCodeBlock) {
					contents = ReplaceWithHtml(contents, "*", "strong");
					contents = ReplaceWithHtml(contents, "_", "em");
					
					if (linkify)
						contents = Linkify(contents);
				}

				if (isInCodeBlock && !nextLineIsCode) {
					suffix = Environment.NewLine + "</pre>";
					isInCodeBlock = false;
				}

				if (!thisLineIsCode && !nextLineIsCode && !prevLineIsCode && !isLast)
					suffix = "<br />";

				lines[i] = string.Concat(prefix, contents, suffix);
			}

			return string.Join(Environment.NewLine, lines);
		}

		private static string ReplaceWithHtml(string input, string delimiter, string tag) {
			delimiter = Regex.Escape(delimiter);

			string regex = string.Format(@"(?<=\W|^){0}([^{0}]+){0}(?=\W|$)", delimiter); // with lookaheads/lookbehinds
			//string regex = string.Format("{0}([^{0}]+){0}", delimiter);

			string output = input;
			Regex r = new Regex(regex);

			foreach (Match match in r.Matches(input)) {
				if (match.Groups.Count > 1) {
					output = output.Replace(match.Groups[0].Value, string.Format("<{1}>{0}</{1}>", match.Groups[1].Value, tag));
				}
			}
			return output;
		}
	}
}
