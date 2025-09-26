using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameFlowManager : MonoSingleton<GameFlowManager>
{
    private CancellationTokenSource _ctsStagePlayTask = new();
    private CancellationTokenSource _ctsWaitGameFailTask = new();

    protected override void Release()
    {
        _ctsStagePlayTask.Cancel();
        _ctsWaitGameFailTask.Cancel();
    }

    public void GameStart()
    {
        SoundManager.Inst.PlayBackgroundMusic(StageData.Inst.BackgroundMusic);

        StagePlayTask(_ctsStagePlayTask.Token).Forget();
        WaitGameFailTask(_ctsWaitGameFailTask.Token).Forget();
    }

    private async UniTaskVoid StagePlayTask(CancellationToken token)
    {
        try
        {
            bool _spawnComplete = false;

            EnemySpawner spawner = StageData.Inst.Spawner;
            spawner.OnSpawnFinished += () => _spawnComplete = true;

            foreach (var data in StageData.Inst.SpawnData)
            {
                StageData.Inst.WaveIsWaiting = false;
                _spawnComplete = false;

                StageData.Inst.WaveCurrentCount++;
                spawner.Play(data);
                EventBus.Inst.Publish(new WaveStartEvent());

                Debug.Log($"WaveStart : {StageData.Inst.WaveCurrentCount} / {StageData.Inst.WaveTotalCount}");

                await UniTask.WaitUntil(() => _spawnComplete, cancellationToken: token);
                StageData.Inst.WaveIsWaiting = true;
                EventBus.Inst.Publish(new WaveEndEvent());

                if (StageData.Inst.WaveCurrentCount >= StageData.Inst.WaveTotalCount)
                {
                    StageData.Inst.WaveIsWaiting = false;
                    break;
                }

                StageData.Inst.WaveRemainTime = StageData.Inst.WaveInterval;
                while ((StageData.Inst.WaveRemainTime -= Time.deltaTime) > 0)
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: token);
            }

            await UniTask.WaitUntil(() => StageData.Inst.ActiveEnemies.Count <= 0, cancellationToken: token);

            _ctsWaitGameFailTask.Cancel();
            GameClear();
        }
        catch (OperationCanceledException) { }
    }
    private async UniTaskVoid WaitGameFailTask(CancellationToken token)
    {
        try
        {
            await UniTask.WaitUntil(() => (StageData.Inst.Core.IsDead), cancellationToken: token);

            _ctsStagePlayTask.Cancel();
            GameFail();
        }
        catch (OperationCanceledException) { }
    }

    private void GameClear()
    {
        StageData.Inst.Spawner.Stop();
        GameData.Inst.GameGold += StageData.Inst.StageReward;

        EventBus.Inst.Publish(new StageClearEvent());
    }
    private void GameFail()
    {
        StageData.Inst.Spawner.Stop();

        EventBus.Inst.Publish(new StageFailEvent());
    }
}
