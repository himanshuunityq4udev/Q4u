using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomPropertyDrawer(typeof(ButtonsName))]
public class EnumWithHeaderDrawer : PropertyDrawer
{

    private float itemHeight = 18f; // Height for each item
    private float maxHeight = 20f; // Max height for the dropdown

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the current enum value
        var enumType = fieldInfo.FieldType;
        var currentValue = (ButtonsName)property.enumValueIndex;

        // Calculate the position for the label and the dropdown button
        float labelWidth = EditorGUIUtility.labelWidth;
        Rect labelRect = new Rect(position.x, position.y, labelWidth, position.height);
        Rect buttonRect = new Rect(position.x + labelWidth, position.y, position.width - labelWidth, position.height);

        // Display the label
        EditorGUI.LabelField(labelRect, label);

        // Create the dropdown button
        if (EditorGUI.DropdownButton(buttonRect, new GUIContent(currentValue.ToString()), FocusType.Keyboard))
        {
            // Create the dropdown menu
            var menu = new GenericMenu();
            var enumValues = System.Enum.GetValues(enumType).Cast<object>().ToList();

            // Add enum items to the menu
            foreach (var value in enumValues)
            {
                var enumMember = enumType.GetField(value.ToString());
                var headerAttribute = enumMember.GetCustomAttributes(typeof(EnumHeaderAttribute), false).FirstOrDefault() as EnumHeaderAttribute;

                // Add a separator for custom headers
                if (headerAttribute != null)
                {
                    menu.AddSeparator($"-- {headerAttribute.Header} --");
                }

                // Add enum items with custom styling
                menu.AddItem(new GUIContent(value.ToString()), value.Equals(currentValue), () =>
                {
                    property.enumValueIndex = (int)value;
                    property.serializedObject.ApplyModifiedProperties();
                });
            }

            // Show the menu as context, but with a specific max height for scrolling
            menu.ShowAsContext();
            var menuPosition = new Rect(buttonRect.x, buttonRect.yMax, buttonRect.width, Mathf.Min(itemHeight * enumValues.Count, maxHeight));
            menuPosition.height = Mathf.Min(menuPosition.height, maxHeight);
        }
    }

    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    // Get the current enum value
    //    var enumType = fieldInfo.FieldType;
    //    var currentValue = (ButtonsName)property.enumValueIndex;

    //    // Calculate the position for the label and the dropdown button
    //    float labelWidth = EditorGUIUtility.labelWidth;
    //    Rect labelRect = new Rect(position.x, position.y, labelWidth, position.height);
    //    Rect buttonRect = new Rect(position.x + labelWidth, position.y, position.width - labelWidth, position.height);

    //    // Display the label
    //    EditorGUI.LabelField(labelRect, label);

    //    // Display the dropdown button
    //    if (EditorGUI.DropdownButton(buttonRect, new GUIContent(currentValue.ToString()), FocusType.Keyboard))
    //    {
    //        // Create the dropdown menu
    //        var menu = new GenericMenu();
    //        var enumValues = System.Enum.GetValues(enumType).Cast<object>().ToList();

    //        foreach (var value in enumValues)
    //        {
    //            // Check for custom headers
    //            var enumMember = enumType.GetField(value.ToString());
    //            var headerAttribute = enumMember.GetCustomAttributes(typeof(EnumHeaderAttribute), false).FirstOrDefault() as EnumHeaderAttribute;

    //            // Add a separator for the header
    //            if (headerAttribute != null)
    //            {
    //                menu.AddSeparator($"-- {headerAttribute.Header} --");
    //            }

    //            // Add the enum value with custom styling
    //            menu.AddItem(new GUIContent(value.ToString()), value.Equals(currentValue), () =>
    //            {
    //                // Update the selected value
    //                property.enumValueIndex = (int)value;
    //                property.serializedObject.ApplyModifiedProperties();
    //            });
    //        }

    //        // Show the menu
    //        menu.ShowAsContext();
    //    }
    //}
}
