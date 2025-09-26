using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
public class SerializableDictionaryDrawer : PropertyDrawer
{
    private readonly float lineHeight = EditorGUIUtility.singleLineHeight;
    private const float padding = 4f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var entriesProp = property.FindPropertyRelative("_entries");
        float totalHeight = lineHeight + padding;

        for (int i = 0; i < entriesProp.arraySize; i++)
        {
            var entry = entriesProp.GetArrayElementAtIndex(i);
            var valueProp = entry.FindPropertyRelative("_value");
            float valueHeight = EditorGUI.GetPropertyHeight(valueProp, GUIContent.none, true);
            totalHeight += Mathf.Max(lineHeight, valueHeight) + padding;
        }

        totalHeight += lineHeight + padding;

        return totalHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty entriesProp = property.FindPropertyRelative("_entries");

        EditorGUI.BeginProperty(position, label, property);
        position.height = lineHeight;
        EditorGUI.LabelField(position, label);

        position.y += lineHeight + padding;

        for (int i = 0; i < entriesProp.arraySize; i++)
        {
            SerializedProperty entry = entriesProp.GetArrayElementAtIndex(i);
            SerializedProperty keyProp = entry.FindPropertyRelative("_key");
            SerializedProperty valueProp = entry.FindPropertyRelative("_value");

            float valueHeight = EditorGUI.GetPropertyHeight(valueProp, GUIContent.none, true);

            Rect keyRect = new Rect(position.x, position.y, position.width * 0.4f, lineHeight);
            Rect valueRect = new Rect(position.x + position.width * 0.45f, position.y, position.width * 0.4f, lineHeight);
            Rect removeRect = new Rect(position.x + position.width - 20f, position.y, 20f, lineHeight);

            EditorGUI.PropertyField(keyRect, keyProp, GUIContent.none, true);
            EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none, true);

            if (GUI.Button(removeRect, "X"))
            {
                entriesProp.DeleteArrayElementAtIndex(i);
                break;
            }

            position.y += Mathf.Max(lineHeight, valueHeight) + padding;
        }

        if (GUI.Button(new Rect(position.x, position.y, position.width, lineHeight), "+"))
            entriesProp.arraySize++;

        EditorGUI.EndProperty();
    }    
}
