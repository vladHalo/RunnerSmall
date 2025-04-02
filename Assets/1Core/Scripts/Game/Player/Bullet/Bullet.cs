using System;
using Core.Scripts.Tools.Extensions;
using Lean.Pool;
using UnityEngine;

namespace _1Core.Scripts.Game.Player.Bullet
{
    public class Bullet : MonoBehaviour
    {
        public float damage;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private TypeBullet[] _arrayBullets;

        private int _typeElementBullet;

        public void Init(int typeElementBullet, int typeBullet, float damage, float speed)
        {
            _typeElementBullet = typeElementBullet;
            this.damage = damage;
            _arrayBullets.ForEach(x => x.typeBullets.ForEach(y => y.SetActive(false)));
            _arrayBullets[typeElementBullet].typeBullets[typeBullet].SetActive(true);
            rb.velocity = Vector3.forward * speed;
        }

        public void Touch()
        {
            switch (_typeElementBullet)
            {
                case 1:
                    BulletParticleFactory.instance.Spawn(transform.position);
                    break;
            }

            LeanPool.Despawn(this);
        }

        [Serializable]
        private class TypeBullet
        {
            public GameObject[] typeBullets;
        }
    }
}