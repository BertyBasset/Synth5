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
    #region Style Properties
    

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

        knbSampleRate.ValueChanged += (o, e) => SampleRateChanged?.Invoke(this, knbSampleRate.Value);
        knbResolution.ValueChanged += (o, e) => ResolutionChanged?.Invoke(this, knbResolution.Value);

        ledSelect.LedChanged += (o, e) => ModuleSelectLedChanged?.Invoke(this, ledSelect.LedOn);
    }
    #endregion
}
