using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrap : MonoBehaviour
{
    private void Start()
    {
        GameData.Inst.LoadData();
        DontDestroyOnLoad(GameData.Inst.gameObject);

        ConfigData.Inst.LoadData();
        DontDestroyOnLoad(ConfigData.Inst.gameObject);

        DontDestroyOnLoad(EventBus.Inst.gameObject);

        DontDestroyOnLoad(FindObjectOfType<SoundManager>().gameObject);
        //DontDestroyOnLoad(FindObjectOfType<OverlayUIManager>().gameObject);

        DontDestroyOnLoad(FindObjectOfType<AudioListener>());
        SceneManager.LoadScene("TitleScene");
    }
}
