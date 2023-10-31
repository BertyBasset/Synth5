using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfUi {
    /// <summary>
    /// Interaction logic for SaveDetails.xaml
    /// </summary>
    public partial class SaveDetails : Window {
        string _Patch;
        string _Bank;

        public class FileSpec {
            public string BankName { get; set; } = "";
            public string PatchName { get; set; } = "";
            public string PatchPath { get; set; } = "";
        }

        public FileSpec? rv;

        public SaveDetails(string Bank, string Patch) {
            _Patch = (Patch ?? "").Trim();
            _Bank = (Bank ?? "").Trim();


            InitializeComponent();
            txtPatch.Text = _Patch;

            cboBanks.ItemsSource = Patching.Patch.GetBanksList();
            cboBanks.SelectedItem = Bank;

            this.PreviewKeyDown += (o, e) => HandleReturnAndCancel(o, e);
            btnSave.Click += (o, e) => SaveData();
            btnCancel.Click += (o, e) => this.Close();
            txtPatch.KeyDown += (o, e) => { if (e.Key == Key.Enter) SaveData(); };
            txtPatch.Focus();
            txtPatch.SelectAll();
        }

        void HandleReturnAndCancel(object o, KeyEventArgs e) {

         if (e.Key == Key.Escape) {
                // Handle Escape key press (e.g., Cancel button behavior).
                this.Close();
            }
        }


        public static bool IsValidFileName(string fileName) {
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            return fileName.IndexOfAny(invalidChars) == -1;
        }

        void SaveData() {
            txtPatch.Text = txtPatch.Text.Trim();
            btnSave.Focus();

            if(cboBanks.SelectedValue == null || (((string)cboBanks.SelectedValue)?? "") == "") {
                MessageBox.Show("Please select a Patch Bank", "Save", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                cboBanks.Focus();
                return;
            }

            if (txtPatch.Text.Trim() == "") {
                MessageBox.Show("Please enter a Patch Name", "Save", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtPatch.Focus();
                return;
            }

            if (!IsValidFileName(txtPatch.Text)) {
                MessageBox.Show("Please enter a valid filename", "Save", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtPatch.Focus();
                txtPatch.SelectAll();
                return;
            }

            if (txtPatch.Text.Trim().ToLower() == _Patch.Trim().ToLower() && ((cboBanks.SelectedValue as string) ?? "") == _Bank.Trim().ToLower()) {
                if (MessageBox.Show($"Overwrite existing patch '{_Patch}'", "CONFIRM!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) {
                    txtPatch.Focus();
                    txtPatch.SelectAll();
                    return;
                }
            }

            var bankName = (cboBanks.SelectedValue as string) ?? "";
            var patchName = txtPatch.Text.Trim();
            var patchesInBank = Patching.Patch.GetPatchListForBank(bankName);

            if (patchesInBank.Any(s => string.Equals(s, patchName, StringComparison.OrdinalIgnoreCase)) == true) {
                if (MessageBox.Show($"Overwrite existing patch '{_Patch}'", "CONFIRM!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) {
                    txtPatch.Focus();
                    txtPatch.SelectAll();
                    return;
                }
            }




            // Save
            rv = new();
            rv.BankName = bankName;
            rv.PatchName = patchName;
            rv.PatchPath = rv.BankName + "\\" + rv.PatchName + ".json";
            this.Close();
        }
    }
}
