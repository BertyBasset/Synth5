//#define WALDORF


using System.Windows.Media;
// Can't get themes to work with usercontrols, so modules, set the properties here, and each separate module can pick them up
// We only need to change the settings here then rather than in each module


namespace WpfUi.Modules {
    internal class ModuleTheme {



#if WALDORF
        // Waldorf
        static readonly Color CAPTION_FOREGROUND = Colors.Blue;
        static readonly Color BORDER_COLOR = Colors.Red;
        static readonly double BORDER_WIDTH = 2.0;
        static readonly double CORNER_RADIUS = 4;
#else

        // Prophet and Oberheim
        static readonly Color CAPTION_FOREGROUND = Colors.White;
        static readonly Color BORDER_COLOR = Colors.White;
        static readonly double BORDER_WIDTH = 1.0;
        static readonly double CORNER_RADIUS = 4;
#endif


        public static Brush CaptionForeground { get { return new SolidColorBrush(CAPTION_FOREGROUND); } }
        public static Color BorderColor { get { return BORDER_COLOR; } }
        public static double BorderWidth { get { return BORDER_WIDTH;  } }
        public static CornerRadius CornerRadius { get { return new CornerRadius(CORNER_RADIUS); } }
    }
}
