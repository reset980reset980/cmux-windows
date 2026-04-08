using System.Globalization;

namespace Cmux.Core.Terminal;

public static class TerminalUnicodeWidth
{
    public static int GetWidth(char ch)
    {
        if (ch == '\0')
            return 0;

        if (char.IsControl(ch))
            return 0;

        var category = CharUnicodeInfo.GetUnicodeCategory(ch);
        if (category is UnicodeCategory.NonSpacingMark or UnicodeCategory.SpacingCombiningMark or UnicodeCategory.EnclosingMark)
            return 0;

        int codePoint = ch;

        if ((codePoint >= 0x1100 && codePoint <= 0x115F) ||
            codePoint == 0x2329 || codePoint == 0x232A ||
            (codePoint >= 0x2E80 && codePoint <= 0xA4CF && codePoint != 0x303F) ||
            (codePoint >= 0xAC00 && codePoint <= 0xD7A3) ||
            (codePoint >= 0xF900 && codePoint <= 0xFAFF) ||
            (codePoint >= 0xFE10 && codePoint <= 0xFE19) ||
            (codePoint >= 0xFE30 && codePoint <= 0xFE6F) ||
            (codePoint >= 0xFF00 && codePoint <= 0xFF60) ||
            (codePoint >= 0xFFE0 && codePoint <= 0xFFE6))
        {
            return 2;
        }

        return 1;
    }

    public static int GetWidth(string text)
    {
        if (string.IsNullOrEmpty(text))
            return 0;

        int width = 0;
        foreach (var ch in text)
            width += Math.Max(0, GetWidth(ch));
        return width;
    }
}
