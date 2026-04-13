using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Stellar.ProjectAnalyzer.Internal;

/// <summary>
/// Finds the text span of a JSON string value by key so Roslyn can put
/// squiggles directly on the value inside the .stellar.project file.
/// </summary>
internal static class JsonTextLocator
{
    /// <summary>
    /// Returns a <see cref="Location"/> that points at the value of
    /// <paramref name="jsonKey"/> in <paramref name="sourceText"/>.
    /// Returns <see cref="Location.None"/> when the key cannot be found.
    /// </summary>
    public static Location FindValueLocation(SourceText sourceText, string filePath, string jsonKey)
    {
        var content = sourceText.ToString();
        var searchKey = $"\"{jsonKey}\"";

        int keyIndex = content.IndexOf(searchKey, System.StringComparison.Ordinal);
        if (keyIndex < 0) return Location.None;

        // Skip past the key token itself
        int pos = keyIndex + searchKey.Length;

        // Skip whitespace and the colon separator
        pos = SkipWhitespace(content, pos);
        if (pos >= content.Length || content[pos] != ':') return Location.None;
        pos++; // consume ':'
        pos = SkipWhitespace(content, pos);

        if (pos >= content.Length) return Location.None;

        int valueStart;
        int valueEnd;

        if (content[pos] == '"')
        {
            // String value — include the quotes so the whole token is underlined.
            valueStart = pos;
            int closingQuote = FindClosingQuote(content, pos + 1);
            if (closingQuote < 0) return Location.None;
            valueEnd = closingQuote + 1; // exclusive
        }
        else
        {
            // Bare value (null / number / bool) — scan until structural character.
            valueStart = pos;
            while (pos < content.Length
                   && content[pos] != ','
                   && content[pos] != '}'
                   && content[pos] != '\n'
                   && content[pos] != '\r')
            {
                pos++;
            }

            valueEnd = pos;
        }

        if (valueEnd <= valueStart) return Location.None;

        var span = TextSpan.FromBounds(valueStart, valueEnd);
        var lineSpan = sourceText.Lines.GetLinePositionSpan(span);

        return Location.Create(filePath, span, lineSpan);
    }

    // ── Helpers ──────────────────────────────────────────────────────────

    private static int SkipWhitespace(string text, int pos)
    {
        while (pos < text.Length
               && (text[pos] == ' ' || text[pos] == '\t'
                                    || text[pos] == '\r' || text[pos] == '\n'))
        {
            pos++;
        }

        return pos;
    }

    /// <summary>
    /// Finds the closing quote of a JSON string, respecting \" escapes.
    /// <paramref name="pos"/> should point to the character AFTER the opening quote.
    /// </summary>
    private static int FindClosingQuote(string text, int pos)
    {
        while (pos < text.Length)
        {
            if (text[pos] == '\\')
            {
                pos += 2;
                continue;
            } // skip escaped char

            if (text[pos] == '"') return pos;
            pos++;
        }

        return -1;
    }
}