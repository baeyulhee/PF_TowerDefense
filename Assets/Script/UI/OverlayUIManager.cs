using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayUIManager : MonoSingleton<OverlayUIManager>
{
    private Dictionary<GameObject, GameObject> _popUpDictionary = new();

    public void ShowPopUp(GameObject popUpPanel)
    {
        if (!_popUpDictionary.TryGetValue(popUpPanel, out GameObject popUpInstance))
        {
            popUpInstance = Instantiate(popUpPanel, transform);
            _popUpDictionary[popUpPanel] = popUpInstance;
        }

        popUpInstance.SetActive(true);
    }
}