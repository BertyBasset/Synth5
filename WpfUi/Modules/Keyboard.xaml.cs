namespace WpfUi.Modules;

public partial class Keyboard : UserControl {
    #region Event declarations
    // Knob value changed events
    public event EventHandler<double>? GlideChanged;
    #endregion

    #region Public properties
    #region Style Properties

    #endregion

    #region Non styled properties
    public string Caption {
        get { return lblCaption.Text; }
        set { lblCaption.Text = value; }
    }
    
    public double Glide {
        get { return knbGlide.Value; }
        set { knbGlide.Value = value;}
    }
    #endregion
    #endregion

    #region Constructor
    public Keyboard() {
        InitializeComponent();

        knbGlide.ValueChanged += (o, e) => GlideChanged?.Invoke(this, knbGlide.Value);
    }
    #endregion
}
