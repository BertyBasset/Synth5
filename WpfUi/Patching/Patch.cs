using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            if (element is UserControl userControl) {
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
        var fName = $"{WpfUi.Utils.Constants.SAVE_LOCATION}patches\\{fileName}";


        // If not found, load init patch instead
        if (!System.IO.File.Exists(fName))
            fName = $"{WpfUi.Utils.Constants.SAVE_LOCATION}patches\\{WpfUi.Utils.Constants.PATCH_INIT_FILE}";

        var json = System.IO.File.ReadAllText(fName);
        
        
        var propertyInfoList = DeserializeFromJson(json);
        if (propertyInfoList == null)
            return;


        // Apply the deserialized properties to the user controls
        ApplyPropertiesToUserControls(canvas, propertyInfoList);
    }

    private static List<UserControlPropertyInfo>? DeserializeFromJson(string json) {
        return JsonSerializer.Deserialize<List<UserControlPropertyInfo>>(json);
    }

    private static void ApplyPropertiesToUserControls(Canvas parent, List<UserControlPropertyInfo> propertyInfoList) {
        foreach (UIElement child in parent.Children) {
            if (child is UserControl userControl) {
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
}