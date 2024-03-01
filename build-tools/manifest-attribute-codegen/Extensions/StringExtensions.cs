using System.Text;
using System.Xml.Linq;
using Xamarin.SourceWriter;

namespace Xamarin.Android.Tools.ManifestAttributeCodeGenerator;

static class StringExtensions
{
	static StringExtensions ()
	{
		// micro unit testing, am so clever!
		if (Hyphenate ("AndSoOn") != "and-so-on")
			throw new InvalidOperationException ("Am so buggy 1 " + Hyphenate ("AndSoOn"));
		if (Hyphenate ("aBigProblem") != "a-big-problem")
			throw new InvalidOperationException ("Am so buggy 2");
		if (Hyphenate ("my-two-cents") != "my-two-cents")
			throw new InvalidOperationException ("Am so buggy 3");
	}

	public static string Hyphenate (this string s)
	{
		var sb = new StringBuilder (s.Length * 2);
		for (int i = 0; i < s.Length; i++) {
			if (char.IsUpper (s [i])) {
				if (i > 0)
					sb.Append ('-');
				sb.Append (char.ToLowerInvariant (s [i]));
			} else
				sb.Append (s [i]);
		}
		return sb.ToString ();
	}

	const string prefix = "AndroidManifest";

	public static string ToActualName (this string s)
	{
		s = s.IndexOf ('.') < 0 ? s : s.Substring (s.LastIndexOf ('.') + 1);

		var ret = (s.StartsWith (prefix, StringComparison.Ordinal) ? s.Substring (prefix.Length) : s).Hyphenate ();
		return ret.Length == 0 ? "manifest" : ret;
	}

	public static bool? GetAsBoolOrNull (this XElement element, string attribute)
	{
		var value = element.Attribute (attribute)?.Value;

		if (value is null)
			return null;

		if (bool.TryParse (value, out var ret))
			return ret;

		return null;
	}

	public static bool GetAttributeBoolOrDefault (this XElement element, string attribute, bool defaultValue)
	{
		var value = element.Attribute (attribute)?.Value;

		if (value is null)
			return defaultValue;

		if (bool.TryParse (value, out var ret))
			return ret;

		return defaultValue;
	}

	public static string GetRequiredAttributeString (this XElement element, string attribute)
	{
		var value = element.Attribute (attribute)?.Value;

		if (value is null)
			throw new InvalidDataException ($"Missing '{attribute}' attribute.");

		return value;
	}

	public static string GetAttributeStringOrEmpty (this XElement element, string attribute)
		=> element.Attribute (attribute)?.Value ?? string.Empty;

	public static string Unhyphenate (this string s)
	{
		if (s.IndexOf ('-') < 0)
			return s;

		var sb = new StringBuilder ();

		for (var i = 0; i < s.Length; i++) {
			if (s [i] == '-') {
				sb.Append (char.ToUpper (s [i + 1]));
				i++;
			} else {
				sb.Append (s [i]);
			}
		}

		return sb.ToString ();
	}

	public static string Capitalize (this string s)
	{
		return char.ToUpper (s [0]) + s.Substring (1);
	}

	public static void WriteAutoGeneratedHeader (this CodeWriter sw)
	{
		sw.WriteLine ("//------------------------------------------------------------------------------");
		sw.WriteLine ("// <auto-generated>");
		sw.WriteLine ("//     This code was generated by 'manifest-attribute-codegen'.");
		sw.WriteLine ("//");
		sw.WriteLine ("//     Changes to this file may cause incorrect behavior and will be lost if");
		sw.WriteLine ("//     the code is regenerated.");
		sw.WriteLine ("// </auto-generated>");
		sw.WriteLine ("//------------------------------------------------------------------------------");
		sw.WriteLine ();
		sw.WriteLine ("#nullable enable"); // Roslyn turns off NRT for generated files by default, re-enable it
	}
}
