using UnityEngine;
using UnityEngine.AI;

public class TowerDetection : MonoBehaviour
{

    public static Enemy DetectByNear(Tower tower, float range)
    {
        Enemy target = null;
        float lastDistance = float.PositiveInfinity;

        foreach (var enemy in StageData.Inst.ActiveEnemies)
        {
            float currentDistance = Vector3.Distance(tower.transform.position, enemy.transform.position);

            if (currentDistance < range && currentDistance < lastDistance)
            {
                target = enemy;
                lastDistance = currentDistance;
            }
        }

        return target;
    }
    public static Enemy DetectByFar(Tower tower, float range)
    {
        Enemy target = null;
        float lastDistance = 0;

        foreach (var enemy in StageData.Inst.ActiveEnemies)
        {
            float currentDistance = Vector3.Distance(tower.transform.position, enemy.transform.position);

            if (currentDistance < range && currentDistance > lastDistance)
            {
                target = enemy;
                lastDistance = currentDistance;
            }
        }

        return target;
    }
    public static Enemy DetectByStrong(Tower tower, float range)
    {
        Enemy target = null;
        float lastHp = 0;

        foreach (var enemy in StageData.Inst.ActiveEnemies)
        {
            float currentDistance = Vector3.Distance(tower.transform.position, enemy.transform.position);
            float currentHp = enemy.CurrentHp;

            if (currentDistance < range && currentHp > lastHp)
            {
                target = enemy;
                lastHp = enemy.CurrentHp;
            }
        }

        return target;
    }
    public static Enemy DetectByWeak(Tower tower, float range)
    {
        Enemy target = null;
        float lastHp = float.PositiveInfinity;

        foreach (var enemy in StageData.Inst.ActiveEnemies)
        {
            float currentDistance = Vector3.Distance(tower.transform.position, enemy.transform.position);
            float currentHp = enemy.CurrentHp;

            if (currentDistance < range && currentHp  < lastHp)
            {
                target = enemy;
                lastHp = enemy.CurrentHp;
            }
        }

        return target;
    }
    public static Enemy DetectByFirst(Tower tower, float range)
    {
        Enemy target = null;
        float lastDistance = float.PositiveInfinity;

        foreach (var enemy in StageData.Inst.ActiveEnemies)
        {
            float currentDistance = Vector3.Distance(tower.transform.position, enemy.transform.position);
            float remainDistance = enemy.GetComponent<NavMeshAgent>().remainingDistance;

            if (currentDistance < range && remainDistance < lastDistance)
            {
                target = enemy;
                lastDistance = currentDistance;
            }
        }

        return target;
    }
    public static Enemy DetectByLast(Tower tower, float range)
    {
        Enemy target = null;
        float lastDistance = 0;

        foreach (var enemy in StageData.Inst.ActiveEnemies)
        {
            float currentDistance = Vector3.Distance(tower.transform.position, enemy.transform.position);
            float remainDistance = enemy.GetComponent<NavMeshAgent>().remainingDistance;

            if (currentDistance < range && remainDistance > lastDistance)
            {
                target = enemy;
                lastDistance = currentDistance;
            }
        }

        return target;
    }
    public static Enemy DetectByRandom(Tower tower, float range)
    {
        Enemy target = null;
        float lastValue = 0;

        foreach (var enemy in StageData.Inst.ActiveEnemies)
        {
            float currentDistance = Vector3.Distance(tower.transform.position, enemy.transform.position);
            float currentValue = Random.value;

            if (currentDistance < range && currentValue > lastValue)
            {
                target = enemy;
                lastValue = currentValue;
            }
        }

        return target;
    }

    public static bool DetectTargetInRange(Tower tower, Enemy target, float range)
    {
        return Vector3.Distance(tower.transform.position, target.transform.position) < range;
    }
}
