using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleSound : MonoBehaviour
{
    [SerializeField] AudioClip _onSound;

    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener((bool val) =>
        {
            if (val && _onSound != null)
                SoundManager.Inst.PlayUISound(_onSound);
        });
    }
}
