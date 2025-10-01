using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroupSection : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons = new();
    [SerializeField] private Button _defaultButton;

    private Button _currentButton;

    public List<Button> Buttons => _buttons;
    public Button DefaultButton => _defaultButton;
    public Button CurrentButton => _currentButton;
    
    private void Start()
    {
        SetActiveButton(_defaultButton);
    }

    public void SetActiveButton(Button button)
    {
        _currentButton = button;
        foreach (var btn in _buttons)
            btn.gameObject.SetActive(btn == button);
    }

#if UNITY_EDITOR
    public void FillButtonsAuto()
    {
        _buttons.Clear();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out Button btn))
                _buttons.Add(btn);
        }

        if (_defaultButton != null && !_buttons.Contains(_defaultButton))
            _defaultButton = null;
    }
#endif
}
