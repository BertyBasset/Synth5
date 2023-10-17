namespace WpfUi.Modules;

public partial class BitCrush : UserControl, ISelectableModule {
    #region Event declarations
    // Knob value changed events
    public event EventHandler<double>? SampleRateChanged;
    public event EventHandler<double>? ResolutionChanged;

    // Module selector led clicked event
    public event EventHandler<bool>? ModuleSelectLedChanged;
    #endregion

    #region Public properties
    #region Styled Properties
    public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
        "CaptionBackgroundBitCrush",
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
         "CaptionForegroundBitCrush",
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
             "BorderColorBitCrush",
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
         "BorderWidthBitCrush",
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
              "CornerRadiusBitCrush",
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

    #region Non Style Properties
    public string Caption {
        get { return lblCaption.Text; }
        set { lblCaption.Text = value; }
    }

    public double SampleRate {
        get { return knbSampleRate.Value; }
        set { knbSampleRate.Value = value;}
    }

    public double Resolution {
        get { return knbResolution.Value; }
        set { knbResolution.Value = value; }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion
    #endregion

    #region Constructor
    public BitCrush() {
        InitializeComponent();

        CaptionForeground = ModuleTheme.CaptionForeground;
        BorderWidth = ModuleTheme.BorderWidth;
        BorderColor = ModuleTheme.BorderColor;
        CornerRadius = ModuleTheme.CornerRadius;

        knbSampleRate.ValueChanged += (o, e) => SampleRateChanged?.Invoke(this, knbSampleRate.Value);
        knbResolution.ValueChanged += (o, e) => ResolutionChanged?.Invoke(this, knbResolution.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}
