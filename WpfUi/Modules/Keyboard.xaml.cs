using SynthCustomControls;

namespace WpfUi.Modules;

public partial class Keyboard : UserControl {
    #region Event declarations
    // Knob value changed events
    public event EventHandler<double>? GlideChanged;
    #endregion

    #region Public properties
    #region Styled Properties
    public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
        "CaptionBackgroundKeyboard",
        typeof(Brush),
        typeof(ADSR),
        new PropertyMetadata(Brushes.Transparent)
    );

    public Brush CaptionBackground {
        get { return (Brush)base.GetValue(CaptionBackgroundProperty); }
        set {
            base.SetValue(CaptionBackgroundProperty, value);
            lblCaption.Background = value;
        }
    }

    public static readonly DependencyProperty CaptionForegroundProperty =
    DependencyProperty.Register(
         "CaptionForegroundKeyboard",
         typeof(Brush),
         typeof(ADSR),
         new PropertyMetadata(null));

    public Brush CaptionForeground {
        get { return (Brush)base.GetValue(CaptionForegroundProperty); }
        set {
            base.SetValue(CaptionForegroundProperty, value);
            lblCaption.Foreground = value;
        }
    }

    public static readonly DependencyProperty BorderColorProperty =
        DependencyProperty.Register(
             "BorderColorKeyboard",
             typeof(Color),
             typeof(ADSR),
             new PropertyMetadata(Colors.White));

    public Color BorderColor {
        get { return (Color)base.GetValue(BorderColorProperty); }
        set {
            base.SetValue(BorderColorProperty, value);
            brdBorder.BorderBrush = new SolidColorBrush(value);
        }
    }

    public static readonly DependencyProperty BorderWidthProperty =
    DependencyProperty.Register(
         "BorderWidthKeyboard",
         typeof(double),
         typeof(ADSR),
         new PropertyMetadata(1.0));

    public double BorderWidth {
        get { return (double)base.GetValue(BorderWidthProperty); }
        set {
            base.SetValue(BorderWidthProperty, value);
            brdBorder.BorderThickness = new Thickness(value);
        }
    }

    public static readonly DependencyProperty CornerRadiusProperty2 =
         DependencyProperty.Register(
              "CornerRadiusKeyboard",
              typeof(CornerRadius),
              typeof(ADSR),
              new PropertyMetadata(new CornerRadius(3)));

    public CornerRadius CornerRadius {
        get { return (CornerRadius)base.GetValue(CornerRadiusProperty2); }
        set {
            base.SetValue(CornerRadiusProperty2, value);
            brdBorder.CornerRadius = value;
        }
    }



    #endregion

    #region Non styled properties

    public (int, bool) KeyState {
        set {
            int keyIndex = value.Item1;
            bool keyDown = value.Item2;

            Led led = led1;
            switch (keyIndex) {
                case 0: led = led1; break;
                case 1: led = led2; break;
                case 2: led = led3; break;
                case 3: led = led4; break;
                case 4: led = led5; break;
                default: break;
            }
            led.Dispatcher.Invoke(() => {
                led.LedOn = keyDown;
            });
        }
    }

    public string Caption {
        get { return lblCaption.Text; }
        set { lblCaption.Text = value; }
    }
    
    public double Glide {
        get { return knbGlide.Value; }
        set { knbGlide.Value = value;}
    }
    #endregion
    #endregion

    #region Constructor
    public Keyboard() {
        InitializeComponent();

        CaptionForeground = ModuleTheme.CaptionForeground;
        BorderWidth = ModuleTheme.BorderWidth;
        BorderColor = ModuleTheme.BorderColor;
        CornerRadius = ModuleTheme.CornerRadius;

        knbGlide.ValueChanged += (o, e) => GlideChanged?.Invoke(this, knbGlide.Value);
    }
    #endregion
}
