using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(OptionalValue<>))]
public class OptionalValueDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var overrideProp = property.FindPropertyRelative("_isOverride");
        var valueProp = property.FindPropertyRelative("_value");

        Rect toggleRect = new Rect(position.x, position.y, 20, position.height);
        Rect fieldRect = new Rect(position.x + 22, position.y, position.width - 22, position.height);

        overrideProp.boolValue = EditorGUI.Toggle(toggleRect, overrideProp.boolValue);

        EditorGUI.BeginDisabledGroup(!overrideProp.boolValue);
        EditorGUI.PropertyField(fieldRect, valueProp, label, true);
        EditorGUI.EndDisabledGroup();
    }
}
