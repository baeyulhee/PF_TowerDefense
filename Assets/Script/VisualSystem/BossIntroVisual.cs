using Cysharp.Threading.Tasks;
using UnityEngine;

public class BossIntroVisual : VisualSystem
{
    [SerializeField] CanvasGroup _introPanel;

    public override void Play()
    {
        BlinkEffect(3, 1f).Forget();
    }

    private async UniTask BlinkEffect(int times = 3, float duration = 1f)
    {
        for (int i = 0; i < times; i++)
        {
            await FadeAlpha(0f, 1f, duration * 0.5f);
            await FadeAlpha(1f, 0f, duration * 0.5f);
        }

        InvokeEndAction();
    }

    private async UniTask FadeAlpha(float from, float to, float time)
    {
        float remainTime = 0f;

        while (remainTime < time)
        {
            remainTime += Time.deltaTime;

            float t = Mathf.Clamp01(remainTime / time);
            _introPanel.alpha = Mathf.Lerp(from, to, t);

            await UniTask.Yield();
        }
    }
}
