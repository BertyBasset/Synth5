﻿using Synth.Properties;

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
          "CaptionBackground",
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
         "CaptionForeground",
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
             "BorderColor ",
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
         "BorderWidth ",
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
              "CornerRadius",
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

    public double Frequency {
        get { return knbFrequency.Value; }
        set { knbFrequency.Value = value; }
    }

    public double Octave {
        get { return knbOctave.Value; }
        set { knbOctave.Value = value; }
    }

    public double Waveform {
        get { return knbWaveform.Value; }
        set { knbWaveform.Value = value; }
    }

    public double PulseWidth {
        get { return knbPulseWidth.Value; }
        set { knbPulseWidth.Value = value; }
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

        knbFrequency.ValueChanged += (o, e) => FrequencyChanged?.Invoke(this, knbFrequency.Value);
        knbOctave.ValueChanged += (o, e) => OctaveChanged?.Invoke(this, (int)knbOctave.Value);
        knbWaveform.ValueChanged += (o, e) => WaveformChanged?.Invoke(this, (VCOWaveformType)knbWaveform.Value);
        knbPulseWidth.ValueChanged += (o, e) => PulseWidthChanged?.Invoke(this, knbPulseWidth.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}