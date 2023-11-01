using System;
using System.Collections.Generic;
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
    /// Interaction logic for PatchManager.xaml
    /// </summary>
    public partial class PatchManager : Window {
        public PatchManager() {
            InitializeComponent();
            LoadBanks();


            AddEventHandlers();
        }

        private void AddEventHandlers() {
            btnClose.Click += (o, e) => this.Close();
            lstBanks.SelectionChanged += (o, e) => LoadPatches();


        }

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

    }
}
