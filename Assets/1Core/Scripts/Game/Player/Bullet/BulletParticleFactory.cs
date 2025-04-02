using _1Core.Scripts.Patterns.Creational.Singleton;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace _1Core.Scripts.Game.Player.Bullet
{
    public class BulletParticleFactory : Singleton<BulletParticleFactory>
    {
        [SerializeField] private Factory<ParticleSystem> _particleFactory;

        public void Spawn(Vector3 position)
        {
            var particle = _particleFactory.Create(position);
            particle.Play();
            DOVirtual.DelayedCall(2, () =>
            {
                particle.Stop();
                LeanPool.Despawn(particle);
            });
        }
    }
}