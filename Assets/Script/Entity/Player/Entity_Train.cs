using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Entity
{
    public abstract class Entity_Train : Entity_Living
    {
        [SerializeField] protected Entity_Train _previousWagon;
        [SerializeField] protected Entity_Wagon _nextWagon;

        public Entity_Train PreviousWagon { get => _previousWagon; set => _previousWagon = value; }
        public Entity_Wagon NextWagon { get => _nextWagon; set => _nextWagon = value; }

        public override void OnHit(int damage)
        {
            base.OnHit(damage);
            ScoreManager.Instance.ScoreUpdate();
        }
    }
}
