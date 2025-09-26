using UnityEngine;

public class ObjectUIElement : MonoBehaviour
{
    public static void SetInteractable<T>(bool value) where T : ObjectUIElement
    {
        foreach (var obj in FindObjectsOfType<T>())
            obj.IsInteractable = value;
    }

    public bool IsInteractable
    {
        get => enabled;
        set => enabled = value;
    }
}
