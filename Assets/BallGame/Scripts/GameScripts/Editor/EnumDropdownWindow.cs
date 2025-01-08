using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class EnumDropdownWindow : EditorWindow
{
    //private SerializedProperty property;
    //private Type enumType;
    //private bool isDragging = false;
    //private Vector2 dragOffset;

    //// Static method to show the dropdown window
    //public static void ShowDropdown(Rect position, SerializedProperty property, Type enumType)
    //{
    //    var window = CreateInstance<EnumDropdownWindow>();
    //    window.property = property;
    //    window.enumType = enumType;

    //    // Calculate dropdown window size and position
    //    window.ShowAsDropDown(position, new Vector2(position.width, 300));
    //}

    //// OnGUI method to draw the contents of the dropdown window
    //private void OnGUI()
    //{
    //    // Title bar
    //    GUILayout.BeginHorizontal(EditorStyles.toolbar);
    //    GUILayout.Label("Select Enum Value", EditorStyles.boldLabel); // Title for the window
    //    if (GUILayout.Button("X", EditorStyles.miniButtonRight))
    //    {
    //        Close(); // Close button
    //    }
    //    GUILayout.EndHorizontal();

    //    // Separator
    //    EditorGUILayout.Space();

    //    // Get all enum values
    //    var enumValues = Enum.GetValues(enumType).Cast<object>().ToList();

    //    foreach (var value in enumValues)
    //    {
    //        // Check for custom headers
    //        var enumMember = enumType.GetField(value.ToString());
    //        var headerAttribute = enumMember.GetCustomAttributes(typeof(EnumHeaderAttribute), false).FirstOrDefault() as EnumHeaderAttribute;

    //        // Display the header if it exists
    //        if (headerAttribute != null)
    //        {
    //            EditorGUILayout.LabelField($"-- {headerAttribute.Header} --", EditorStyles.boldLabel);
    //        }

    //        // Create a custom style for enum values
    //        var style = new GUIStyle(GUI.skin.button)
    //        {
    //            alignment = TextAnchor.MiddleLeft,
    //            fontSize = 12,
    //            normal = { textColor = Color.blue }
    //        };

    //        // Display the enum value as a clickable button
    //        if (GUILayout.Button(value.ToString(), style))
    //        {
    //            // Update the selected value
    //            property.enumValueIndex = Array.IndexOf(Enum.GetValues(enumType), value);
    //            property.serializedObject.ApplyModifiedProperties();

    //            // Close the dropdown after selection
    //            Close();
    //        }
    //    }

    //    // Handle dragging the window
    //    HandleWindowDrag();
    //}

    //// Method to handle dragging the window
    //private void HandleWindowDrag()
    //{
    //    // Check if the mouse is clicked within the window and start dragging
    //    if (Event.current.type == EventType.MouseDown && position.Contains(Event.current.mousePosition))
    //    {
    //        isDragging = true;
    //        dragOffset = Event.current.mousePosition - position.position;
    //        Event.current.Use(); // Mark the event as used
    //    }
    //    else if (Event.current.type == EventType.MouseUp)
    //    {
    //        isDragging = false;
    //    }

    //    // If dragging, update the position of the window
    //    if (isDragging)
    //    {
    //        Vector2 currentMousePosition = Event.current.mousePosition;
    //        position = new Rect(currentMousePosition - dragOffset, position.size);
    //        Repaint(); // Redraw the window
    //    }
    //}
}
