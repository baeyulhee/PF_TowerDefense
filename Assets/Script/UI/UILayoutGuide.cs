using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(RawImage))]
public class UILayoutGuide : MonoBehaviour
{
    [SerializeField] Color _color;
    private RawImage _rawImage;

#if UNITY_EDITOR
    private void OnEnable()
    {
        _rawImage = GetComponent<RawImage>();
        _rawImage.raycastTarget = false;
        UpdateGuide();
    }

    private void Update()
    {
        if (Application.isPlaying)
            _rawImage.enabled = false;
        else
        {
            _rawImage.enabled = true;
            UpdateGuide();
        }
    }

    private void UpdateGuide()
    {
        _rawImage ??= GetComponent<RawImage>();
        _rawImage.color = _color;

        RectTransform rectTransform = GetComponent<RectTransform>();
    }
#endif
}
