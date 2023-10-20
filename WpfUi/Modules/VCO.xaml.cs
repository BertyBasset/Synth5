using Synth;
using Synth.Properties;
using System.Text.Json.Serialization;
using WpfUi.Utils;

namespace WpfUi.Modules;
public partial class VCO : UserControl, ISelectableModule {
    #region Event declarations
    // Knob value changed events
    public event EventHandler<double>? FrequencyChanged;
    public event EventHandler<int>? OctaveChanged;
    public event EventHandler<VCOWaveformType>? WaveformChanged;
    public event EventHandler<double>? PulseWidthChanged;
    // Module selector led clicked event
    public event EventHandler<bool>? ModuleSelectLedChanged;
    #endregion

    #region Public properties
    #region Style Properties
    public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
          "CaptionBackgroundVCO",
          typeof(Brush),
          typeof(TextBlock),
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
         "CaptionForegroundVCO",
         typeof(Brush),
         typeof(TextBlock),
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
             "BorderColorVCO",
             typeof(Color),
             typeof(VCO),
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
         "BorderWidthVCO",
         typeof(double),
         typeof(VCO),
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
              "CornerRadiusVCO",
              typeof(CornerRadius),
              typeof(VCO),
              new PropertyMetadata(new CornerRadius(3)));

    public CornerRadius CornerRadius {
        get { return (CornerRadius)base.GetValue(CornerRadiusProperty2); }
        set {
            base.SetValue(CornerRadiusProperty2, value);
            brdBorder.CornerRadius = value;
        }
    }

    #endregion


    #region Non style properties
    public string Caption {
        get { return lblCaption.Text; }
        set { lblCaption.Text = value; }
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
    public double Octave {
        get { return knbOctave.Value; }
        set { 
            knbOctave.Value = value;
            OctaveChanged?.Invoke(this, (int)value);
        }
    }

    [JsonSerializeAttribute]
    public double Waveform {
        get { return knbWaveform.Value; }
        set { 
            knbWaveform.Value = value;
            WaveformChanged?.Invoke(this, (VCOWaveformType)value);
        }
    }

    [JsonSerializeAttribute]
    public double PulseWidth {
        get { return knbPulseWidth.Value; }
        set { 
            knbPulseWidth.Value = value;
            PulseWidthChanged?.Invoke(this, value);
        }
    }

    public bool ModuleSelectLedOn{
        get { return ledSelect.LedOn; }
        set { ledSelect.LedOn = value; }
    }
    #endregion
    #endregion

    #region Constructor
    public VCO() {
        InitializeComponent();

        CaptionForeground = ModuleTheme.CaptionForeground;
        BorderWidth = ModuleTheme.BorderWidth;
        BorderColor = ModuleTheme.BorderColor;
        CornerRadius = ModuleTheme.CornerRadius;

        knbFrequency.ValueChanged += (o, e) => FrequencyChanged?.Invoke(this, knbFrequency.Value);
        knbOctave.ValueChanged += (o, e) => OctaveChanged?.Invoke(this, (int)knbOctave.Value);
        knbWaveform.ValueChanged += (o, e) => WaveformChanged?.Invoke(this, (VCOWaveformType)knbWaveform.Value);
        knbPulseWidth.ValueChanged += (o, e) => PulseWidthChanged?.Invoke(this, knbPulseWidth.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}