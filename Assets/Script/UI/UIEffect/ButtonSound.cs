using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip _clickSound;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_clickSound)
                SoundManager.Inst.PlayUISound(_clickSound);
        });
    }
}