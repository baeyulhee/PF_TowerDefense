using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRequestManager : MonoSingleton<PlayerRequestManager>
{
    //Request Tower Control
    public void RequestTowerBuild(TowerSlot towerSlot, TowerData towerData, int cost)
    {
        Tower tower = TowerManager.Inst.Create(towerData);
        towerSlot.AttachTower(tower);

        tower.SpentCost += cost;
        StageData.Inst.Point -= cost;
    }
    public void RequestTowerSell(Tower tower, int sellPrice)
    {
        tower.Remove();
        StageData.Inst.Point += sellPrice;
    }
    public void RequestTowerUpgrade(Tower tower, int cost)
    {
        tower.UpgradeSystem.ApplyUpgrade(tower, 1);

        tower.SpentCost += cost;
        StageData.Inst.Point -= cost;
    }

    //Request Game Control
    public void RequestExitGame()
    {
        SoundManager.Inst.StopBackgroundMusic();
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
    public void RequestWaveStart()
    {
        if (StageData.Inst.WaveCurrentCount == 0)
            GameFlowManager.Inst.GameStart();
        else
            StageData.Inst.WaveRemainTime = 0;
    }
    public void RequestGamePause()
    {
        Time.timeScale = 0.1f;
        StageData.Inst.IsPause = true;
    }
    public void RequestGameResume()
    {
        Time.timeScale = 1f;
        StageData.Inst.IsPause = false;
    }
}
