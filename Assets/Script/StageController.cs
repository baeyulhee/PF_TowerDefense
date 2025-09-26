using UnityEngine;

public class StageController : MonoBehaviour
{
    void Start()
    {
        var towreFactory = TowerManager.Inst;
        var enemyFactory = EnemyManager.Inst;

        var playerRequestManager = PlayerRequestManager.Inst;

        StageData.Inst.InitializeData(GameData.Inst.CurrentStage);
        EventBus.Inst.Subscribe<EnemyKillEvent>(OnEnemyKillEvent);
    }
    private void OnDestroy()
    {
        EventBus.Inst.UnSubscribe<EnemyKillEvent>(OnEnemyKillEvent);
    }

    private void OnEnemyKillEvent(EnemyKillEvent evt)
    {
        StageData.Inst.Point += evt.Enemy.Point;
    }
}