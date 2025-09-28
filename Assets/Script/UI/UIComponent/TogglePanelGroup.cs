using System.Collections.Generic;
using UnityEngine;

public class TogglePanelGroup : MonoBehaviour
{
    [SerializeField] List<UIPanel> _panelList = new();
    [SerializeField] UIPanel _openedPanel;

    public UIPanel OpenedPanel => _openedPanel;

    private void Awake()
    {
        foreach (var panel in _panelList)
        {
            panel.OnShow += () =>
            {
                CloseNotToggledPanels(panel);
                _openedPanel = panel;
            };

            if (panel != _openedPanel)
                panel.Hide();
        }
    }

    private void CloseNotToggledPanels(UIPanel selectedPanel)
    {
        foreach (var panel in _panelList)
        {
            if (panel == selectedPanel)
                continue;
            panel.Hide();
        }
    }
}
