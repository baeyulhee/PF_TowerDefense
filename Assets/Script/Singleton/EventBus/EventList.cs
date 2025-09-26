
#region GameFlowEvent

public struct WaveStartEvent
{ }
public struct WaveEndEvent
{ }

public struct StageClearEvent
{

}
public struct StageFailEvent
{

}
#endregion

#region Object LifeCycle
public struct TowerCreateEvent
{
    public Tower Tower { get; }
    public TowerCreateEvent(Tower tower) { Tower = tower; }
}
public struct TowerDestroyEvent
{
    public Tower Tower { get; }
    public TowerDestroyEvent(Tower tower) { Tower = tower; }
}
public struct EnemyCreateEvent
{
    public Enemy Enemy { get; }
    public EnemyCreateEvent(Enemy enemy) { Enemy = enemy; }
}
public struct EnemyDestroyEvent
{
    public Enemy Enemy { get; }
    public EnemyDestroyEvent(Enemy enemy) { Enemy = enemy; }
}

public struct EnemyKillEvent
{
    public Enemy Enemy { get; }
    public EnemyKillEvent(Enemy enemy) { Enemy = enemy; }
}
#endregion

#region Stage UI Event
public struct RequestStageSelectEvent
{ }
public struct RequestMainUpgradeEvent
{ }
public struct RequestConfigEvent
{ }

public struct RequestStageMenuEvent
{ }

public struct TowerSelectEvent
{
    public Tower Tower { get; }
    public TowerSelectEvent(Tower tower) { Tower = tower; }
}
public struct TowerSlotSelectEvent
{
    public TowerSlot TowerSlot { get; }
    public TowerSlotSelectEvent(TowerSlot towerSlot) { TowerSlot = towerSlot; }
}
#endregion