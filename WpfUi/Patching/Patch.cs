using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfUi.Utils;
using Constants = WpfUi.Utils.Constants;

namespace WpfUi.Patching;

[AttributeUsage(AttributeTargets.Property)]
public class JsonSerializeAttribute : Attribute {
}

public class UserControlPropertyInfo {
    public string UserControlName { get; set; } = "";
    public string PropertyName { get; set; } = "";
    public object? PropertyValue { get; set; }
}


class Patch {
    #region Save
    public static void SavePatch(string fileName, UIElement parent) {
        var propertyInfoList = SerializeUserControlProperties(parent);

        // Serialize the property information to JSON
        var json = JsonSerializer.Serialize(propertyInfoList);
        System.IO.File.WriteAllText($"{WpfUi.Utils.Constants.SAVE_LOCATION}patches\\{fileName}", json);

    }


    private static List<UserControlPropertyInfo> SerializeUserControlProperties(UIElement parent) {
        var propertyInfoList = new List<UserControlPropertyInfo>();

        foreach (UIElement element in LogicalTreeHelper.GetChildren(parent).OfType<UIElement>()) {
            if (element is System.Windows.Controls.UserControl userControl) {
                string userControlName = userControl.Name;

                var properties = userControl.GetType().GetProperties();

                foreach (var property in properties) {
                    if (Attribute.IsDefined(property, typeof(JsonSerializeAttribute))) {
                        var propertyInfo = new UserControlPropertyInfo {
                            UserControlName = userControlName,
                            PropertyName = property.Name,
                            PropertyValue = property.GetValue(userControl)
                        };

                        propertyInfoList.Add(propertyInfo);
                    }
                }
            }

            propertyInfoList.AddRange(SerializeUserControlProperties(element));
        }

        return propertyInfoList;
    }
    #endregion

    #region Retrieve
    public static void LoadPatch(string fileName, Canvas canvas) {
        var fName = $"{WpfUi.Utils.Constants.PATCH_LOCATION}{fileName}";


        // If not found, load init patch instead
        if (!System.IO.File.Exists(fName))
            fName = $"{WpfUi.Utils.Constants.PATCH_LOCATION}{WpfUi.Utils.Constants.PATCH_INIT_FILE}";

        var json = System.IO.File.ReadAllText(fName);


        var propertyInfoList = DeserializeFromJson(json);
        if (propertyInfoList == null)
            return;


        // Apply the deserialized properties to the user controls
        ApplyPropertiesToUserControls(canvas, propertyInfoList);
    }

    private static List<UserControlPropertyInfo>? DeserializeFromJson(string json) {
        try {
            return JsonSerializer.Deserialize<List<UserControlPropertyInfo>>(json);
        } catch {
            System.Windows.MessageBox.Show("Invalid Json", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return null;
        }
    }

    private static void ApplyPropertiesToUserControls(Canvas parent, List<UserControlPropertyInfo> propertyInfoList) {
        foreach (UIElement child in parent.Children) {
            if (child is System.Windows.Controls.UserControl userControl) {
                string userControlName = userControl.Name;
                var userControlType = userControl.GetType();

                foreach (var propertyInfo in propertyInfoList) {
                    if (propertyInfo.PropertyValue != null && propertyInfo.UserControlName == userControlName) {
                        var property = userControlType.GetProperty(propertyInfo.PropertyName);

                        if (property != null && property.CanWrite) {
                            if (property.PropertyType == typeof(double)) {
                                if (propertyInfo.PropertyValue is JsonElement jsonElement) {
                                    if (jsonElement.TryGetDouble(out double doubleValue)) {
                                        property.SetValue(userControl, doubleValue);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region Patch Management
    public static List<string> GetBanksList() {
        // Where banks are subfolder
        var patchRoot = $"{Constants.PATCH_LOCATION}";
        var folders = System.IO.Directory.GetDirectories(patchRoot);
        return folders.Select(f => System.IO.Path.GetFileName(f)).ToList();
    }

    public static List<string> GetPatchListForBank(string Bank) {
        // Where banks are subfolder
        var bankRoot = $"{Constants.PATCH_LOCATION}{Bank}";

        var files = System.IO.Directory.GetFiles(bankRoot, "*.json");
        return files.Select(f => System.IO.Path.GetFileNameWithoutExtension(f)).ToList();
    }

    public static void CreateBank(string BankName) {
        System.IO.Directory.CreateDirectory($"{Constants.PATCH_LOCATION}{BankName}");
    }

    public static void RenameBank(string OldBankName, string NewBankName) {
        System.IO.Directory.Move($"{Constants.PATCH_LOCATION}{OldBankName}", $"{Constants.PATCH_LOCATION}{NewBankName}");
    }

    public static void DeleteBank(string BankName) {
        System.IO.Directory.Delete($"{Constants.PATCH_LOCATION}{BankName}", true);
    }

    public static void RenamePatch(string BankName, string OldPatchName, string NewPatchName) {
        // Strip json off names, in case theyre there

        OldPatchName = System.IO.Path.GetFileNameWithoutExtension(OldPatchName) + ".json";
        NewPatchName = System.IO.Path.GetFileNameWithoutExtension(NewPatchName) + ".json";

        System.IO.File.Move($"{Constants.PATCH_LOCATION}{BankName}\\{OldPatchName}", $"{Constants.PATCH_LOCATION}{BankName}\\{NewPatchName}");
    }

    public static void DeletePatch(string BankName, string PatchName) {
        PatchName = System.IO.Path.GetFileNameWithoutExtension(PatchName) + ".json";
        System.IO.File.Delete($"{Constants.PATCH_LOCATION}{BankName}\\{PatchName}");
    }

    public static void MovePatch(string SourceBank, string DestBank, string PatchName) { 
        string sourceFile = $"{Constants.PATCH_LOCATION}{SourceBank}\\{System.IO.Path.GetFileNameWithoutExtension(PatchName)}.json";
        string destFile = $"{Constants.PATCH_LOCATION}{DestBank}\\{System.IO.Path.GetFileNameWithoutExtension(PatchName)}.json";
        System.IO.File.Move(sourceFile , destFile );
    }

    public static void CopyPatch(string Bank, string SourceName, string DestName) {
        string sourceFile = $"{Constants.PATCH_LOCATION}{Bank}\\{System.IO.Path.GetFileNameWithoutExtension(SourceName)}.json";
        string destFile = $"{Constants.PATCH_LOCATION}{Bank}\\{System.IO.Path.GetFileNameWithoutExtension(DestName)}.json";
        System.IO.File.Copy(sourceFile, destFile);
    }
     


    #endregion

}