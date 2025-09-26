#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class ForceBootSceneOnPlay
{
    const string BOOT_SCENE_PATH = "Assets/Scenes/BootScene.unity";
    static bool _sceneLoadedManually = false;

    static ForceBootSceneOnPlay()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode && !_sceneLoadedManually)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                _sceneLoadedManually = true;

                EditorApplication.isPlaying = false; // Play 취소
                EditorSceneManager.OpenScene(BOOT_SCENE_PATH); // BootScene 열기

                EditorApplication.delayCall += () =>
                {
                    EditorApplication.isPlaying = true; // 다시 Play
                    _sceneLoadedManually = false;
                };
            }
        }
    }
}
#endif