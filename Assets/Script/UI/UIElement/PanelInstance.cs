using System;
using UnityEngine;

[Serializable]
public class PanelInstance<T> where T : PanelUI
{
    [SerializeField] RectTransform _parent;
    [SerializeField] T _prefab;
    private T _instance;

    public RectTransform Parent => _parent;
    public T Prefab => _prefab;
    public T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Instantiate(_prefab, _parent);
                _instance.Init();
                _instance.gameObject.SetActive(false);
            }

            return _instance;
        }
    }
}
