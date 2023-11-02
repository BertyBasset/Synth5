using System.Linq;

namespace WpfUi;

    
public partial class Prompt : Window {
    public string? NewName { get; private set; }


    public enum ModeType { 
        AddBank,
        RenameBank,
        RenamePatch,
    }

    private readonly ModeType _mode;
    private readonly string? _currentBankName;
    public Prompt(ModeType Mode, string? CurrentBankName = null, string? CurrentPatchName =null) {
        _mode = Mode;
        _currentBankName = CurrentBankName;

        InitializeComponent();
        AddEventHandlers();

        switch (Mode) {
            case ModeType.AddBank: Title = "Add Bank"; lblTitle.Content = "Bank Name:"; break;
            case ModeType.RenameBank: Title = "Rename Bank"; lblTitle.Content = "Bank Name:"; txtName.Text = CurrentBankName; break;
            case ModeType.RenamePatch: Title = "Rename Patch"; lblTitle.Content = "Patch Name:"; txtName.Text = CurrentPatchName; break;
            default: break;
        }

        txtName.Focus();

        if((txtName.Text??"").Length > 0 )
            txtName.SelectAll();
        else
            btnOK.IsEnabled = false;
    }

    private void AddEventHandlers() {
        txtName.TextChanged += (o, e) => btnOK.IsEnabled = txtName.Text.Trim() != "";
    }

    private void OK_Click(object sender, RoutedEventArgs e) {
        txtName.Text = txtName.Text.Trim();

        if (!IsValidFileName(txtName.Text)) {
            MessageBox.Show($"Invalid {(_mode == ModeType.RenamePatch ? "Patch" : "Bank")} Name", "Save", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            txtName.SelectAll();
            txtName.Focus();
            return;
        }

        // Check bank exists if renaming
        if (_mode == ModeType.AddBank || _mode == ModeType.RenameBank) {
            var existingBanks = Patching.Patch.GetBanksList();
            if (existingBanks.FirstOrDefault(s => s.Equals(txtName.Text, StringComparison.OrdinalIgnoreCase)) != null) {
                MessageBox.Show($"Bank '{txtName.Text}' already exists.", "Save", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtName.SelectAll();
                txtName.Focus();
                return;
            }
        }

        // Check patch exists if renaming
        if (_mode == ModeType.RenamePatch) {
            var existingPatches = Patching.Patch.GetPatchListForBank(_currentBankName??"");
            if (existingPatches.FirstOrDefault(s => s.Equals(txtName.Text, StringComparison.OrdinalIgnoreCase)) != null) {
                MessageBox.Show($"Patch '{txtName.Text}' already exists in Bank '{_currentBankName}'.", "Save", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtName.SelectAll();
                txtName.Focus();
                return;
            }
        }



        NewName = txtName.Text;
        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e) {
        DialogResult = false;
    }

    public static bool IsValidFileName(string fileName) {
        char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
        return fileName.IndexOfAny(invalidChars) == -1;
    }
}

