using System;

namespace CMarkSharp
{
	enum cmark_options
	{
		Default = 0,
		SourcePositions = 1,
		HardBreaks = 2,
		Normalize = 4,
		Smart = 8,
		ValidateUTF8 = 16
	};

	public struct CMarkOptions
	{
		public bool SourcePositions { get; set; }
		public bool HardBreaks { get; set; }
		public bool Normalize { get; set; }
		public bool Smart { get; set; }
		public bool ValidateUTF8 { get; set; }

		internal cmark_options ToOptions()
		{
			cmark_options options = cmark_options.Default;

			if (SourcePositions) options |= cmark_options.SourcePositions;
			if (HardBreaks)      options |= cmark_options.HardBreaks;
			if (Normalize)       options |= cmark_options.Normalize;
			if (Smart)           options |= cmark_options.Smart;
			if (ValidateUTF8)    options |= cmark_options.ValidateUTF8;

			return options;
		}
	}
}

