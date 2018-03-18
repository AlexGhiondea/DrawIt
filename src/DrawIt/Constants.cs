
using System.Drawing;

namespace DrawIt
{
    public static class Constants
    {
        public static class Application
        {
            public static class Header
            {
                public const string Text = "App_HeaderText";
                public const string FontSize = "App_HeaderFontSize";
                public const string FontName = "App_HeaderFontName";
                public const string FontStyle = "App_HeaderFontStlye";
                public const string TextColor = "App_HeaderTextColor";
            }

            public static class Logo
            {
                public const string Image = "App_LogoImage";
                public const string Height = "App_LogoHeight";
            }

            public static class Defaults
            {
                public const int LogoHeight = 8;
                public const float HeaderTextSize = 22f;
            }
        }

        public static class Document
        {
            public const string MeasurementUnit = "MeasurementUnit";
            public const string ConversionRate = "ConversionRate";

            public static class Defaults
            {
                public const string MeasurementUnitDefault = "square foot";
            }
        }

        public static class Measurement
        {
            public const string BelowOrAbove = "Measurement_BelowOrAbove";
            public const string Color = "Measurement_Color";

            public static class Defaults
            {
                public const int Green = unchecked((int)0xff008000);
                public const int Black = unchecked((int)0xff000000);
                public const int DrawWidth = 2;
                public const int ArcSize = 4;
                public const float FontSize = 10f;
                public const string FontFamily = "Calibri";
            }
        }

        public static class Draw
        {
            public const string Color = "Draw_Color";
            public const string Width = "Draw_Width";
            public const string DrawObject = "Draw_DrawObject";

            public static class Arc
            {
                public const string Radius = "Draw_Arc_Radius";
            }
        }
        public static class Text
        {
            public const string Color = "Text_Color";
            public const string FontName = "Text_FontName";
            public const string FontSize = "Text_FontSize";
            public const string FontStyle = "Text_FontStyle";
        }
    }
}
