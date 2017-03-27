using System;
using System.Drawing;

namespace DrawIt
{
    public static class FontHelpers
    {
        public static Font GetHeaderFont()
        {
            float fontSize = Configuration.GetSettingOrDefault<float>(Constants.Application.Header.FontSize, float.TryParse, Constants.Application.Defaults.HeaderTextSize);
            string fontName = Configuration.GetSetting(Constants.Application.Header.FontName) ?? "Calibri";
            FontStyle fontStyle = Configuration.GetSettingOrDefault<FontStyle>(Constants.Application.Header.FontStyle, Enum.TryParse<FontStyle>, FontStyle.Regular);

            return new Font(fontName, fontSize, fontStyle);
        }
    }
}
