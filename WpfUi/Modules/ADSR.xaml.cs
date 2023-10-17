namespace WpfUi.Modules;

public partial class ADSR : UserControl, ISelectableModule {
    #region Event declarations
    public event EventHandler<bool>? ModuleSelectLedChanged;
    // Knob value changed events
    public event EventHandler<double>? AttackChanged;
    public event EventHandler<double>? DecayChanged;
    public event EventHandler<double>? SustainChanged;
    public event EventHandler<double>? ReleaseChanged;

    #endregion

    #region Public Properties
    #region Styled Properties
    public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
        "CaptionBackgroundAdsr",
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
         "CaptionForegroundAdsr",
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
             "BorderColorAdsr",
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
         "BorderWidthAdsr",
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
              "CornerRadiusAdsr",
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

    public double Attack {
        get { return knbAttack.Value; }
        set { knbAttack.Value = value;}
    }

    public double Decay {
        get { return knbDecay.Value; }
        set { knbDecay.Value = value; }
    }

    public double Sustain {
        get { return knbSustain.Value; }
        set { knbSustain.Value = value; }
    }

    public double Release {
        get { return knbRelease.Value; }
        set { knbRelease.Value = value; }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion
    #endregion

    #region Constructor
    public ADSR() {
        InitializeComponent();

        CaptionForeground = ModuleTheme.CaptionForeground;
        BorderWidth = ModuleTheme.BorderWidth;
        BorderColor = ModuleTheme.BorderColor;
        CornerRadius = ModuleTheme.CornerRadius;


        knbAttack.ValueChanged += (o, e) => AttackChanged?.Invoke(this, knbAttack.Value);
        knbDecay.ValueChanged += (o, e) => DecayChanged?.Invoke(this, knbDecay.Value);
        knbSustain.ValueChanged += (o, e) => SustainChanged?.Invoke(this, knbSustain.Value);
        knbRelease.ValueChanged += (o, e) => ReleaseChanged?.Invoke(this, knbRelease.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}
