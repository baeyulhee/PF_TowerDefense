using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] AudioClip _shootSound;
    public event Action<Enemy> OnHitEnemy;

    private Target<Enemy> _target = new();
    private float _speed;
    private float _lifeSpan;

    public void Launch(Action<Enemy> onHitEnemy = null, Enemy target = null, float speed = 20f, float lifeSpan = 5f)
    {
        OnHitEnemy = onHitEnemy;
        _target.Attach(target);
        _speed = speed;
        _lifeSpan = lifeSpan;

        GetComponentInChildren<TrailRenderer>()?.Clear();
        SoundManager.Inst.PlaySoundEffect(_shootSound);
    }

    private void Update()
    {
        if (_target.IsTargeting)
            transform.LookAt(_target.Transform.position);

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        if ((_lifeSpan -= Time.deltaTime) < 0)
        {
            _target.Detach();
            Poolable.TryReturn(this);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy == _target.Data)
            {
                OnHitEnemy?.Invoke(enemy);

                _target.Detach();
                Poolable.TryReturn(this);
            }
        }
    }

    private void OnDisable()
    {
        OnHitEnemy = null;
    }
}
