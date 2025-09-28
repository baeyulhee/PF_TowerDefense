using System;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    private Action _openAction;
    private Action _closeAction;

    public event Action OnShow;
    public event Action OnHide;

    private bool _isInitialized = false;

    private RectTransform _rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            _rectTransform ??= GetComponent<RectTransform>();
            return _rectTransform;
        }
    }

    private void Awake()
    {
        if (!_isInitialized)
        {
            Init();
            _isInitialized = true;
        }
    }

    public abstract void Init();
    public virtual void Show()
    {
        if (!_isInitialized)
        {
            Init();
            _isInitialized = true;
        }
        OnShow?.Invoke();
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        OnHide?.Invoke();
        gameObject.SetActive(false);
    }
    
    public void Open()
    {
        if (_openAction != null)
            _openAction();
        else
            Show();
    }
    public void Close()
    {
        if (_closeAction != null)
            _closeAction();
        else
            Hide();
    }
    public void SetOpenAction(Action openAction)
    {
        _openAction = openAction;
    }
    public void SetCloseAction(Action closeAction)
    {
        _closeAction = closeAction;
    }
}