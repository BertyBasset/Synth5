namespace WpfUi;

public partial class PatchManager : Window {

    #region Constructor
    public PatchManager() {
        InitializeComponent();
        LoadBanks();
        AddEventHandlers();
    }
    #endregion

    #region Init
    void LoadBanks() {
        var banks = Patching.Patch.GetBanksList();
        lstBanks.ItemsSource = banks;

        if (lstBanks.Items.Count > 0) {
            lstBanks.SelectedIndex = 0;
            LoadPatches();
        }
    }

    void LoadPatches() {
        if (lstBanks.SelectedValue == null)
            return;

        var patches = Patching.Patch.GetPatchListForBank((string)lstBanks.SelectedValue);
        lstPatches.ItemsSource = patches;

        if (lstPatches.Items.Count > 0)
            lstPatches.SelectedIndex = 0;
    }
    #endregion

    #region Event Handlers
    private void AddEventHandlers() {
        btnClose.Click += (o, e) => this.Close();

        lstBanks.SelectionChanged += (o, e) => {
            LoadPatches();

            if (lstBanks.SelectedIndex == -1) {
                btnDeleteBank.IsEnabled = false;
                btnRenameBank.IsEnabled = false;
            } else {
                btnDeleteBank.IsEnabled = true;
                btnRenameBank.IsEnabled = true;
            }
        };

        lstPatches.SelectionChanged += (o, e) => {
            if (lstPatches.SelectedIndex == -1) {
                btnDeletePatch.IsEnabled = false;
                btnRenamePatch.IsEnabled = false;
            } else {
                btnDeletePatch.IsEnabled = true;
                btnRenamePatch.IsEnabled = true;
            }
        };

        #region Add Bank
        btnAddBank.Click += (o, e) => AddBank();

        AddBankMenuItem.Click += (sender, e) => {
            AddBank();
        };
        #endregion

        #region Rename Bank
        btnRenameBank.Click += (o, e) => {
            if (lstBanks.SelectedItem is string selectedBank)
                RenameBank(selectedBank);
        };

        lstBanks.MouseDoubleClick += (o, e) => {
            if (lstBanks.SelectedItem is string selectedBank)
                RenameBank(selectedBank);
        };

        RenameBankMenuItem.Click += (sender, e) => {
            if (lstBanks.SelectedItem is string selectedBank) {
                RenameBank(selectedBank);
            }
        };

        #endregion

        #region Delete Bank
        btnDeleteBank.Click += (o, e) => {
            if (lstBanks.SelectedItem is string selectedBank)
                DeleteBank(selectedBank);
        };

        lstBanks.KeyUp += (o, e) => {
            if (e.Key == System.Windows.Input.Key.Delete) {
                if (lstBanks.SelectedItem is string selectedBank)
                    DeleteBank(selectedBank);
            }
        };

        DeleteBankMenuItem.Click += (sender, e) => {
            if (lstBanks.SelectedItem is string selectedBank)
                DeleteBank(selectedBank);
        };


        #endregion

        #region Rename Patch
        btnRenamePatch.Click += (o, e) => {
            if (lstBanks.SelectedItem is string selectedBank) {
                if (lstPatches.SelectedItem is string selectedPatch) {
                    RenamePatch(selectedBank, selectedPatch);
                }
            }
        };

        lstPatches.MouseDoubleClick += (o, e) => {
            if (lstBanks.SelectedItem is string selectedBank) {
                if (lstPatches.SelectedItem is string selectedPatch) {
                    RenamePatch(selectedBank, selectedPatch);
                }
            }
        };

        RenamePatchMenuItem.Click += (sender, e) => {
            if (lstBanks.SelectedItem is string selectedBank) {
                if (lstPatches.SelectedItem is string selectedPatch) {
                    RenamePatch(selectedBank, selectedPatch);
                }
            }
        };
        #endregion

        #region Delete Patch
            btnDeletePatch.Click += (o, e) => {
            if (lstBanks.SelectedItem is string selectedBank) {
                if (lstPatches.SelectedItem is string selectedPatch) {
                    DeletePatch(selectedBank, selectedPatch);
                }
            }
        };

        lstPatches.KeyUp += (o, e) => {
            if (e.Key == System.Windows.Input.Key.Delete) {
                if (lstBanks.SelectedItem is string selectedBank) {
                    if (lstPatches.SelectedItem is string selectedPatch) {
                        DeletePatch(selectedBank, selectedPatch);
                    }
                }
            }
        };

        DeletePatchMenuItem.Click += (sender, e) => {
            if (lstBanks.SelectedItem is string selectedBank) {
                if (lstPatches.SelectedItem is string selectedPatch) {
                    DeletePatch(selectedBank, selectedPatch);
                }
            }
        };
        #endregion

        }
    #endregion

    #region Methods
    void AddBank() {
        var prompt = new Prompt(Prompt.ModeType.AddBank);
        bool? result = prompt.ShowDialog();

        if (result == false || string.IsNullOrWhiteSpace(prompt.NewName))
            return;

        // Add folder
        Patching.Patch.CreateBank(prompt.NewName);

        // Update Banks
        LoadBanks();
        lstBanks.SelectedValue = prompt.NewName;
    }

    void RenameBank(string CurrentBankName) {
        if (string.IsNullOrWhiteSpace(CurrentBankName))
            return;

        var prompt = new Prompt(Prompt.ModeType.RenameBank, CurrentBankName);
        
        bool? result = prompt.ShowDialog();

        if (result == false || string.IsNullOrWhiteSpace(prompt.NewName))
            return;

        // Rename folder
        Patching.Patch.RenameBank(CurrentBankName, prompt.NewName);

        // Update Banks
        LoadBanks();
        lstBanks.SelectedValue = prompt.NewName;

    }



    void DeleteBank(string BankName) {
        if (string.IsNullOrWhiteSpace(BankName))
            return;
     
    }

    void DeletePatch(string BankName, string PatchName) {
        if (string.IsNullOrWhiteSpace(BankName) || string.IsNullOrWhiteSpace(PatchName))
            return;
     
    }

    void RenamePatch(string CurrentBankName, string PatchName) {
        if (string.IsNullOrWhiteSpace(CurrentBankName) || string.IsNullOrWhiteSpace(CurrentBankName) || string.IsNullOrWhiteSpace(PatchName) || string.IsNullOrWhiteSpace(PatchName))
            return;

        var prompt = new Prompt(Prompt.ModeType.RenamePatch, CurrentBankName, PatchName);

        bool? result = prompt.ShowDialog();

        if (result == false || string.IsNullOrWhiteSpace(prompt.NewName))
            return;

        // Rename file
        Patching.Patch.RenamePatch(CurrentBankName, PatchName, prompt.NewName);

        // Reload Patches
        LoadPatches();

        // Select filename
        lstPatches.SelectedValue = prompt.NewName;
    }
    #endregion
}