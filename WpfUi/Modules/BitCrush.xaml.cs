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
    public string Caption {
        get { return lblCaption.Text; }
        set { lblCaption.Text = value; }
    }

    public Brush CaptionBrush {
        get { return lblCaption.Foreground; }
        set { 
            lblCaption.Foreground = value; 
        }
    }

    public Brush BorderColor {
        get { return brdBorder.BorderBrush; }
        set { brdBorder.BorderBrush = value; }
    }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Brush BorderBrush {
        get { return BorderColor; }
        set { BorderColor = value; }
    }

    public Thickness BorderWidth{ 
        get { return brdBorder.BorderThickness; }
        set  {brdBorder.BorderThickness = value; }
    }

    public CornerRadius BorderRadius {
        get { return brdBorder.CornerRadius; }
        set { brdBorder.CornerRadius = value; }
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

    #region Constructor
    public BitCrush() {
        InitializeComponent();

        knbSampleRate.ValueChanged += (o, e) => SampleRateChanged?.Invoke(this, knbSampleRate.Value);
        knbResolution.ValueChanged += (o, e) => ResolutionChanged?.Invoke(this, knbResolution.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}
