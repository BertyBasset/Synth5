namespace WpfUi.Modules;
public partial class Mixer : UserControl, ISelectableModule {
    #region Event declarations
    // Knob value changed events
    public event EventHandler<double>? Osc1LevelChanged;
    public event EventHandler<double>? Osc2LevelChanged;
    public event EventHandler<double>? Osc3LevelChanged;
    public event EventHandler<double>? NoiseLevelChanged;
    // Module selector led clicked event
    public event EventHandler<bool>? ModuleSelectLedChanged;

    #endregion

    #region Public properties
    #region Styled Properties
    public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
        "CaptionBackgroundMixer",
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
         "CaptionForegroundMixer",
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
             "BorderColorMixer",
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
         "BorderWidthMixer",
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
              "CornerRadiusMixer",
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

    #region Non styled rpoperties
    public string Caption {
        get { return lblCaption.Text; }
        set { lblCaption.Text = value; }
    }
      
    public double Osc1 {
        get { return knbOsc1.Value; }
        set { knbOsc1.Value = value;}
    }

    public double Osc2{
        get { return knbOsc2.Value; }
        set { knbOsc2.Value = value; }
    }

    public double Osc3 {
        get { return knbOsc3.Value; }
        set { knbOsc3.Value = value; }
    }

    public double Noise {
        get { return knbNoise.Value; }
        set { knbNoise.Value = value; }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion
    #endregion

    #region Constructor
    public Mixer() {
        InitializeComponent();

        CaptionForeground = ModuleTheme.CaptionForeground;
        BorderWidth = ModuleTheme.BorderWidth;
        BorderColor = ModuleTheme.BorderColor;
        CornerRadius = ModuleTheme.CornerRadius;

        knbOsc1.ValueChanged += (o, e) => Osc1LevelChanged?.Invoke(this, knbOsc1.Value);
        knbOsc2.ValueChanged += (o, e) => Osc2LevelChanged?.Invoke(this, knbOsc2.Value);
        knbOsc3.ValueChanged += (o, e) => Osc3LevelChanged?.Invoke(this, knbOsc3.Value);
        knbNoise.ValueChanged += (o, e) => NoiseLevelChanged?.Invoke(this, knbNoise.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}
