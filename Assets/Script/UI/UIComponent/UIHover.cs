using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static UIHover Get(GameObject obj)
    {
        return obj.TryGetComponent(out UIHover hover) ? hover : obj.AddComponent<UIHover>();
    }

    public UnityEvent onEnter { get; } = new UnityEvent();
    public UnityEvent onExit { get; } = new UnityEvent();

    public void OnPointerEnter(PointerEventData eventData) => onEnter.Invoke();
    public void OnPointerExit(PointerEventData eventData) => onExit.Invoke();
}