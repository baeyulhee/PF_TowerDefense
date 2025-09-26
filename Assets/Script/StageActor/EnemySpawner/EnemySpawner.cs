using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class SpawnInfo
{
    public EnemyData EnemyInfo;
    public int Amount;
    public float Interval;

    public SpawnInfo(EnemyData enemyInfo, int amount, float interval)
    {
        EnemyInfo = enemyInfo;
        Amount = amount;
        Interval = interval;
    }
}

public class EnemySpawner : MonoBehaviour
{
    public event Action OnSpawnFinished;
    private CancellationTokenSource _ctsSpawnTask = new();

    private void OnDisable()
    {
        OnSpawnFinished = null;
    }

    private void Spawn(EnemyData enemyData)
    {
        Enemy enemy = EnemyManager.Inst.Create(enemyData);
        enemy.Place(transform.position, transform.rotation);
    }
    public void Play(EnemyData enemyData, int amount, float interval)
    {
        SpawnTask(new SpawnInfo(enemyData, amount, interval), _ctsSpawnTask.Token).Forget();
    }
    public void Play(SpawnInfo spawnInfo)
    {
        SpawnTask(spawnInfo, _ctsSpawnTask.Token).Forget();
    }

    public void Stop()
    {
        _ctsSpawnTask.Cancel();
    }

    private async UniTaskVoid SpawnTask(SpawnInfo spawnInfo, CancellationToken token)
    {
        try
        {
            for (int i = 0; i < spawnInfo.Amount; i++)
            {
                Spawn(spawnInfo.EnemyInfo);
                if (i < spawnInfo.Amount - 1)
                    await UniTask.Delay(TimeSpan.FromSeconds(spawnInfo.Interval), cancellationToken: token);
            }

            OnSpawnFinished?.Invoke();
        }
        catch (OperationCanceledException) { }
    }
}