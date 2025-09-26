using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] string _key;
    public string Key => _key;

    [SerializeField] Enemy _prefab;
    public Enemy Prefab => _prefab;

    [SerializeField] int _point;
    public int Point => _point;

    [SerializeField] float _maxHp;
    public float MaxHp => _maxHp;

    [SerializeField] float _speed;
    public float Speed => _speed;
}
