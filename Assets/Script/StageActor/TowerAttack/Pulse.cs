using System;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] AudioClip _shootSound;
    public event Action<Enemy> OnHitEnemy;

    private float _range;
    private float _lifeSpan;

    public void Launch(Action<Enemy> onHitEnemy = null, float range = 5f, float lifeSpan = 1f)
    {
        _lifeSpan = lifeSpan;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _range);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Enemy enemy))
                OnHitEnemy?.Invoke(enemy);
        }

        _particleSystem.transform.localScale = new Vector3(_range / 2, 1, _range / 2);
        _particleSystem.Play();
        SoundManager.Inst.PlaySoundEffect(_shootSound);
    }

    private void Update()
    {
        if ((_lifeSpan -= Time.deltaTime) < 0)
        {
            _particleSystem.Stop();
            Poolable.TryReturn(this);
        }
    }
}
