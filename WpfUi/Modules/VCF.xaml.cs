using Synth;
using WpfUi.Utils;

namespace WpfUi.Modules;
public partial class VCF : UserControl, ISelectableModule {
    #region Event declarations
    // Knob value changed events
    public event EventHandler<Enums.FilterType>? FilterTypeChanged;
    public event EventHandler<double>? CutoffChanged;
    public event EventHandler<double>? ResonanceChanged;
    public event EventHandler<double>? EnvelopeAmountChanged;

    // Module selector led clicked event
    public event EventHandler<bool>? ModuleSelectLedChanged;
    #endregion

    #region Public properties
    #region Styled Properties
    public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
        "CaptionBackgroundVCF",
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
         "CaptionForegroundVCF",
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
             "BorderColorVCF",
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
         "BorderWidthVCF",
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
              "CornerRadiusVCF",
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
    private string _caption = "VCF";
    public string Caption {
        get { return _caption; }
        set { 
            _caption = value;
            UpdateFilterCaption();
        }
    }

    [JsonSerializeAttribute]
    public double FilterType {
        get { return knbFilterType.Value; }
        set { 
            knbFilterType.Value = value;
            FilterTypeChanged?.Invoke(this, (Enums.FilterType)value);
        }
    }

    [JsonSerializeAttribute]
    public double Cutoff {
        get { return knbCutoff.Value; }
        set {
            knbCutoff.Value = value;
            CutoffChanged?.Invoke(this, value);
        }
    }

    [JsonSerializeAttribute]
    public double Resonance {
        get { return knbResonance.Value; }
        set { 
            knbResonance.Value = value;
            ResonanceChanged?.Invoke(this, value);
        }
    }

    [JsonSerializeAttribute]
    public double EnvelopeAmount {
        get { return knbEnvelopeAmount.Value; }
        set { 
            knbEnvelopeAmount.Value = value; 
            EnvelopeAmountChanged?.Invoke(this, value);
        }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion
    #endregion

    #region Constructor
    public VCF() {
        InitializeComponent();

        CaptionForeground = ModuleTheme.CaptionForeground;
        BorderWidth = ModuleTheme.BorderWidth;
        BorderColor = ModuleTheme.BorderColor;
        CornerRadius = ModuleTheme.CornerRadius;

        knbFilterType.ValueChanged += (o, e) => { FilterTypeChanged?.Invoke(this, (Enums.FilterType)knbFilterType.Value); UpdateFilterCaption(); };
        knbCutoff.ValueChanged += (o, e) => CutoffChanged?.Invoke(this, knbCutoff.Value);
        knbResonance.ValueChanged += (o, e) => ResonanceChanged?.Invoke(this, knbResonance.Value);
        knbEnvelopeAmount.ValueChanged += (o, e) => EnvelopeAmountChanged?.Invoke(this, knbEnvelopeAmount.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion

    private void UpdateFilterCaption() {
        lblCaption.Text = " " + _caption.Trim() +" - " + ((Enums.FilterType)knbFilterType.Value).ToString() + " ";
    }
}
