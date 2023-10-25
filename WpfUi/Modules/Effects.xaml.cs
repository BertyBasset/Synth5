using Synth;
using Synth.Core;
using System.Windows.Media;
using WpfUi.Utils;

namespace WpfUi.Modules;

public partial class Effects : UserControl, ISelectableModule {
    #region Event declarations
    // Knob value changed events
    // Pass enums back here
    public event EventHandler<Enums.EffectType>? EffectTypeChanged;
    public event EventHandler<double>? GainChanged;
    public event EventHandler<double>? FrequencyChanged;
    public event EventHandler<double>? MixChanged;

    // Module selector led clicked event
    public event EventHandler<bool>? ModuleSelectLedChanged;
    #endregion

    #region Public properties

    #region Styled Properties
    public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
        "CaptionBackgroundEffects",
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
         "CaptionForegroundEffects",
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
             "BorderColorEffects",
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
         "BorderWidthEffects",
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
              "CornerRadiusEffects",
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
    private string _caption = "MODULE";
    public string Caption {
        get { return _caption; }
        set {
            _caption = value;
            UpdateEffectsCaption();
        }
    }

    [JsonSerializeAttribute]
    public double EffectType {
        get { return knbEffectType.Value; }
        set { 
            knbEffectType.Value = value;
            EffectTypeChanged?.Invoke(this, (Enums.EffectType)  value);
        }
    }

    [JsonSerializeAttribute]
    public double Gain {
        get { return knbGain.Value; }
        set { 
            knbGain.Value = value;
            GainChanged?.Invoke(this, value);
        }
    }

    [JsonSerializeAttribute]
    public double Frequency {
        get { return knbFrequency.Value; }
        set { 
            knbFrequency.Value = value;
            FrequencyChanged?.Invoke(this, value);
        }
    }

    [JsonSerializeAttribute]
    public double Mix {
        get { return knbMix.Value; }
        set { 
            knbMix.Value = value;
            MixChanged?.Invoke(this, value);
        }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion
    #endregion

    #region Constructor
    public Effects() {
        InitializeComponent();

        CaptionForeground = ModuleTheme.CaptionForeground;
        BorderWidth = ModuleTheme.BorderWidth;
        BorderColor = ModuleTheme.BorderColor;
        CornerRadius = ModuleTheme.CornerRadius;

        knbEffectType.ValueChanged += (o, e) => { 
            EffectTypeChanged?.Invoke(this, (Enums.EffectType)knbEffectType.Value); UpdateEffectsCaption(); 
        };
        knbGain.ValueChanged += (o, e) => GainChanged?.Invoke(this, knbGain.Value);
        knbFrequency.ValueChanged += (o, e) => FrequencyChanged?.Invoke(this, knbFrequency.Value);
        knbMix.ValueChanged += (o, e) => MixChanged?.Invoke(this, knbMix.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion

    private void UpdateEffectsCaption() {
        lblCaption.Text = " " + _caption.Trim() + " - " + ((Enums.EffectType)knbEffectType.Value).ToString().ToCapitalizedWithSpaces() + " ";
    }


}
