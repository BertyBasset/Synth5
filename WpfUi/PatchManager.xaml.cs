using System.Windows.Input;

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
        lstBanks.Items.Clear();
        foreach(var b in banks) 
            lstBanks.Items.Add(b);

        if (lstBanks.Items.Count > 0) {
            lstBanks.SelectedIndex = 0;
            LoadPatches();
        }

        if (lstBanks.Items.Count > 0) {
            btnDeleteBank.IsEnabled = true;
            btnRenameBank.IsEnabled = true;
            DeleteBankMenuItem.IsEnabled = true;
            RenameBankMenuItem.IsEnabled = true;
        } else {
            btnDeleteBank.IsEnabled = false;
            btnRenameBank.IsEnabled = false;
            DeleteBankMenuItem.IsEnabled = false;
            RenameBankMenuItem.IsEnabled = false;

        }
    }

    void LoadPatches() {
        if (lstBanks.SelectedValue == null)
            return;
        lstPatches.ItemsSource = null;
        var patches = Patching.Patch.GetPatchListForBank((string)lstBanks.SelectedValue);
        lstPatches.ItemsSource = patches;

        if (lstPatches.Items.Count > 0)
            lstPatches.SelectedIndex = 0;


        if (lstPatches.Items.Count > 0) {
            btnCopyPatch.IsEnabled = true;
            btnDeletePatch.IsEnabled = true;
            btnRenamePatch.IsEnabled = true;
            CopyPatchMenuItem.IsEnabled = true;
            DeletePatchMenuItem.IsEnabled = true;
            RenamePatchMenuItem.IsEnabled = true;

        } else {
            btnCopyPatch.IsEnabled = false;
            btnDeletePatch.IsEnabled = false;
            btnRenamePatch.IsEnabled = false;
            CopyPatchMenuItem.IsEnabled = false;
            DeletePatchMenuItem.IsEnabled = false;
            RenamePatchMenuItem.IsEnabled = false;

        }
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
                DeleteBankMenuItem.IsEnabled = false;
                RenameBankMenuItem.IsEnabled = false;

            } else {
                btnDeleteBank.IsEnabled = true;
                btnRenameBank.IsEnabled = true;
                DeleteBankMenuItem.IsEnabled = true;
                RenameBankMenuItem.IsEnabled = true;

            }
        };

        lstPatches.SelectionChanged += (o, e) => {
            if (lstPatches.SelectedIndex == -1) {
                btnCopyPatch.IsEnabled = false;
                btnDeletePatch.IsEnabled = false;
                btnRenamePatch.IsEnabled = false;
                CopyPatchMenuItem.IsEnabled = false;
                DeletePatchMenuItem.IsEnabled = false;
                RenamePatchMenuItem.IsEnabled = false;
            } else {
                btnCopyPatch.IsEnabled = true;
                btnDeletePatch.IsEnabled = true;
                btnRenamePatch.IsEnabled = true;
                CopyPatchMenuItem.IsEnabled = true;
                DeletePatchMenuItem.IsEnabled = true;
                RenamePatchMenuItem.IsEnabled = true;

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

        #region Copy Patch
        btnCopyPatch.Click += (o, e) => {
            if (lstBanks.SelectedItem is string selectedBank) {
                if (lstPatches.SelectedItem is string selectedPatch) {
                    CopyPatch(selectedBank, selectedPatch);
                }
            }
        };

        CopyPatchMenuItem.Click += (sender, e) => {
            if (lstBanks.SelectedItem is string selectedBank) {
                if (lstPatches.SelectedItem is string selectedPatch) {
                    CopyPatch(selectedBank, selectedPatch);
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

        if (lstBanks.Items.Count <= 1) {
            MessageBox.Show("There must be at least one Banks defined", "Delete Bank", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return;
        }

        var patchesInBank = Patching.Patch.GetPatchListForBank(BankName);
        if (patchesInBank.Count == 0) {
            if (MessageBox.Show($"Delete Bank '{BankName}' ?", "Delete Bank", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;
        } else {
            if (MessageBox.Show($"Bank '{BankName}' contains {patchesInBank.Count} Patches. Are you sure you want to delete bank?", "Delete Bank", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) 
                return;
            if (MessageBox.Show($"Please confirm. Delete bank '{BankName}' ?", "Delete Bank", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) 
                return;
        }

        Patching.Patch.DeleteBank(BankName);

        // Reload Patches
        LoadBanks();
        LoadPatches();
    }

    void CopyPatch(string BankName, string PatchName) {
        if (string.IsNullOrWhiteSpace(BankName) || string.IsNullOrWhiteSpace(PatchName))
            return;

        var prompt = new Prompt(Prompt.ModeType.CopyPatch, BankName, PatchName);

        bool? result = prompt.ShowDialog();

        if (result == false || string.IsNullOrWhiteSpace(prompt.NewName))
            return;

        Patching.Patch.CopyPatch(BankName, PatchName, prompt.NewName);

        // Reload Patches
        LoadPatches();

        lstPatches.SelectedValue = prompt.NewName;
    }


    void DeletePatch(string BankName, string PatchName) {
        if (string.IsNullOrWhiteSpace(BankName) || string.IsNullOrWhiteSpace(PatchName))
            return;
     
        if(MessageBox.Show($"Delete Patch '{PatchName}' ?", "Delete Patch", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

        Patching.Patch.DeletePatch(BankName, PatchName);

        // Reload Patches
        LoadBanks();
        LoadPatches();

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

    #region Drag and Drop

    string? sourceBank;
    private void LstBanks_Drop(object sender, DragEventArgs e) {
        if (e.Data.GetDataPresent(typeof(string))) {
            string patchName = (string)e.Data.GetData(typeof(string));

            if (lstBanks.SelectedValue != null && patchName != null && sourceBank != null) {
                string destBank = (string)lstBanks.SelectedValue;

                if (destBank == sourceBank) {
                    MessageBox.Show("Source and destination banks sre the same", "Move Patch", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                
                Patching.Patch.MovePatch(sourceBank, destBank, patchName);

                LoadBanks();
                LoadPatches();
                lstBanks.SelectedValue = destBank;
            }

        }
        sourceBank = null;
    }

    private void LstPatches_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
        ListBox listBox = (ListBox)sender;
        object? data = GetDataFromListBox(listBox, e.GetPosition(listBox));
        if (data != null) {
            listBox.SelectedValue = data;
            DragDrop.DoDragDrop(listBox, data, DragDropEffects.Move);
        }
    }

    private void LstPatches_PreviewMouseMove(object sender, MouseEventArgs e) {
        if (lstBanks.SelectedValue != null)
            sourceBank = (string)lstBanks.SelectedValue;

        if (e.LeftButton == MouseButtonState.Pressed) {
            ListBox listBox = (ListBox)sender;
            object? data = GetDataFromListBox(listBox, e.GetPosition(listBox));
            if (data != null) {
                DragDrop.DoDragDrop(listBox, data, DragDropEffects.Move);
            }
        }
    }

    private static object? GetDataFromListBox(ListBox listBox, Point point) {
        UIElement? element = listBox.InputHitTest(point) as UIElement;
        if (element != null) {
            object data = DependencyProperty.UnsetValue;
            while (data == DependencyProperty.UnsetValue) {
                data = listBox.ItemContainerGenerator.ItemFromContainer(element);

                if (data == DependencyProperty.UnsetValue) {
                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }

                if (element == listBox) {
                    return null;
                }
            }

            if (data != DependencyProperty.UnsetValue) {
                return data;
            }
        }

        return null;
    }

    private void LstBanks_DragOver(object sender, DragEventArgs e) {
        
            e.Effects = DragDropEffects.Move;
                        // Find the item under the mouse pointer
            Point position = e.GetPosition(lstBanks);
            var item = FindItemUnderMouse(lstBanks, position);

            if (item != null && item != hoveredItem) {
                // Update the selection to the item being hovered over
                lstBanks.SelectedItem = item;
                hoveredItem = item;
            }

            e.Handled = true;
        
    }
     
    private object? hoveredItem;
    private static object? FindItemUnderMouse(ListBox listBox, Point position) {
            HitTestResult hitTestResult = VisualTreeHelper.HitTest(listBox, position);
            if (hitTestResult != null) {
                DependencyObject obj = hitTestResult.VisualHit;

                while (obj != null && obj != listBox) {
                    if (obj is ListBoxItem) {
                        return ((ListBoxItem)obj).DataContext;
                    }
                    obj = VisualTreeHelper.GetParent(obj);
                }
            }
            return null;
        }

        #endregion

    }