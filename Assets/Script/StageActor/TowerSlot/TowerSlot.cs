using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    Tower _boundTower;
    public Tower BoundTower => _boundTower;

    public bool IsPlaced => _boundTower != null;

    public void AttachTower(Tower tower)
    {
        _boundTower = tower;
        _boundTower.Place(transform.position, transform.rotation);
        _boundTower.OnRemoved += DetachTower;

        GetComponent<Collider>().enabled = false;
    }
    public void DetachTower()
    {
        _boundTower.OnRemoved -= DetachTower;
        _boundTower = null;

        GetComponent<Collider>().enabled = true;
    }
}
