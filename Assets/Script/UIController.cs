using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] BossIntroVisual _bossIntroVisual;

    private void Start()
    {
        EventBus.Inst.Subscribe<WaveStartEvent>(OnWaveStart);
    }
    private void OnDestroy()
    {
        EventBus.Inst.UnSubscribe<WaveStartEvent>(OnWaveStart);
    }

    private void OnWaveStart(WaveStartEvent evt)
    {
        if (StageData.Inst.WaveCurrentCount >= StageData.Inst.WaveTotalCount)
        {
            var effect = Instantiate(_bossIntroVisual, transform);
            effect.EndAction += () =>  Destroy(effect.gameObject);
            effect.Play();
        }
    }
}
