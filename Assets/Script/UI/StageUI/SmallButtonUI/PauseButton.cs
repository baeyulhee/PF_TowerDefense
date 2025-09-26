using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] Button _btnMenu;

    private void Start()
    {
        _btnMenu.onClick.AddListener(() => EventBus.Inst.Publish(new RequestStageMenuEvent()));
    }
}
