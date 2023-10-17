using Synth.Properties;

namespace WpfUi.Modules;

public partial class DualLfo : UserControl, ISelectableModule {
    #region Event declarations    
    // Knob value changed events
    public event EventHandler<double>? Rate1Changed;
    public event EventHandler<LFOWaveformType>? Shape1Changed;
    public event EventHandler<double>? Delay1Changed;
    public event EventHandler<double>? Rate2Changed;
    public event EventHandler<LFOWaveformType>? Shape2Changed;
    public event EventHandler<double>? Delay2Changed;

    // Module selector led clicked event
    public event EventHandler<bool>? ModuleSelectLedChanged;
    #endregion

    #region Public properties
    #region Styled Properties
    public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
        "CaptionBackgroundDualLfo",
        typeof(Brush),
        typeof(ADSR),
        new PropertyMetadata(Brushes.Transparent)
    );

    public Brush CaptionBackground {
        get { return (Brush)base.GetValue(CaptionBackgroundProperty); }
        set {
            base.SetValue(CaptionBackgroundProperty, value);
            lblCaption1.Background = value;
            lblCaption2.Background = value;
        }
    }

    public static readonly DependencyProperty CaptionForegroundProperty =
    DependencyProperty.Register(
         "CaptionForegroundDualLfo",
         typeof(Brush),
         typeof(ADSR),
         new PropertyMetadata(null));

    public Brush CaptionForeground {
        get { return (Brush)base.GetValue(CaptionForegroundProperty); }
        set {
            base.SetValue(CaptionForegroundProperty, value);
            lblCaption1.Foreground = value;
            lblCaption2.Foreground = value;
        }
    }

    public static readonly DependencyProperty BorderColorProperty =
        DependencyProperty.Register(
             "BorderColorDualLfo",
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
         "BorderWidthDualLfo",
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
              "CornerRadiusDualLfo",
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

    #region Non Styled Properties
    public string Caption1 {
        get { return lblCaption1.Text; }
        set { lblCaption1.Text = value; }
    }

    public string Caption2 {
        get { return lblCaption2.Text; }
        set { lblCaption2.Text = value; }
    }
     
    public double Rate1 {
        get { return knbRate1.Value; }
        set { knbRate1.Value = value;}
    }

    public double Shape1 {
        get { return knbShape1.Value; }
        set { knbShape1.Value = value; }
    }

    public double Delay1 {
        get { return knbDelay1.Value; }
        set { knbDelay1.Value = value; }
    }

    public double Rate2 {
        get { return knbRate2.Value; }
        set { knbRate2.Value = value; }
    }

    public double Shape2 {
        get { return knbShape2.Value; }
        set { knbShape2.Value = value; }
    }

    public double Delay2 {
        get { return knbDelay2.Value; }
        set { knbDelay2.Value = value; }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion
    #endregion

    #region Constructor
    public DualLfo() {
        InitializeComponent();

        CaptionForeground = ModuleTheme.CaptionForeground;
        BorderWidth = ModuleTheme.BorderWidth;
        BorderColor = ModuleTheme.BorderColor;
        CornerRadius = ModuleTheme.CornerRadius;

        knbRate1.ValueChanged += (o, e) => Rate1Changed?.Invoke(this, knbRate1.Value);
        knbShape1.ValueChanged += (o, e) => Shape1Changed?.Invoke(this, (LFOWaveformType)knbShape1.Value);
        knbDelay1.ValueChanged += (o, e) => Delay1Changed?.Invoke(this, knbDelay1.Value);
        knbRate2.ValueChanged += (o, e) => Rate2Changed?.Invoke(this, knbRate2.Value);
        knbShape2.ValueChanged += (o, e) => Shape2Changed?.Invoke(this, (LFOWaveformType)knbShape2.Value);
        knbDelay2.ValueChanged += (o, e) => Delay2Changed?.Invoke(this, knbDelay2.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}
