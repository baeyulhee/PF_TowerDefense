using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ObsValue<>))]
public class ObsValueDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var valueProp = property.FindPropertyRelative("_value");

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(position, valueProp, label);
        if (EditorGUI.EndChangeCheck() && Application.isPlaying)
        {
            object target = property.serializedObject.targetObject;

            var obsValueInstance = fieldInfo.GetValue(target);
            var valuePropInfo = obsValueInstance.GetType().GetProperty("Value");

            valuePropInfo.SetValue(obsValueInstance, valueProp.boxedValue);
        }
    }
}