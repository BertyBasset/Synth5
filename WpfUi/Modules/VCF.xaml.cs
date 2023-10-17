using Synth;

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
    #region Style Properties
    

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

    public double FilterType {
        get { return knbFilterType.Value; }
        set { knbFilterType.Value = value;}
    }

    public double Cutoff {
        get { return knbCutoff.Value; }
        set { knbCutoff.Value = value; }
    }

    public double Resonance {
        get { return knbResonance.Value; }
        set { knbResonance.Value = value; }
    }

    public double EnvelopeAmount {
        get { return knbEnvelopeAmount.Value; }
        set { knbEnvelopeAmount.Value = value; }
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
