using UnityEngine;

public class PanelUI : MonoBehaviour
{
    private RectTransform _rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            _rectTransform ??= GetComponent<RectTransform>();
            return _rectTransform;
        }
    }

    public bool IsActive => gameObject.activeSelf;

    public virtual void Init()
    {

    }
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    protected virtual void Refresh()
    {

    }
}