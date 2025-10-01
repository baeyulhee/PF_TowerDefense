#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButtonGroupSection))]
public class ButtonGroupSectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var buttonsProp = serializedObject.FindProperty("_buttons");
        EditorGUILayout.PropertyField(buttonsProp);

        var defaultButtonProp = serializedObject.FindProperty("_defaultButton");
        var buttonGroup = (ButtonGroupSection)target;

        if (buttonGroup.Buttons != null && buttonGroup.Buttons.Count > 0)
        {
            string[] options = new string[buttonGroup.Buttons.Count + 1];
            options[0] = "(None)";

            int currentIndex = 0;

            for (int i = 0; i < buttonGroup.Buttons.Count; i++)
            {
                var btn = buttonGroup.Buttons[i];
                options[i + 1] = btn != null ? btn.name : "(null)";
                if (btn == buttonGroup.DefaultButton)
                    currentIndex = i + 1;
            }

            int newIndex = EditorGUILayout.Popup("Default Button", currentIndex, options);

            if (newIndex == 0)
                defaultButtonProp.objectReferenceValue = null;
            else if (newIndex > 0 && newIndex <= buttonGroup.Buttons.Count)
                defaultButtonProp.objectReferenceValue = buttonGroup.Buttons[newIndex - 1];
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif