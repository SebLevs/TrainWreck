using System;
using UnityEngine;

namespace Entity
{
    public abstract class Entity_Origin : MonoBehaviour
    {
        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            OnStart();
            Init();
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.IsPaused)
            {
                OnFixedUpdate();
            }
        }

        private void Update()
        {
            if (!GameManager.Instance.IsPaused)
            {
                OnUpdate();
            }
        }

        protected abstract void OnAwake();
        protected abstract void OnStart();
        protected abstract void OnFixedUpdate();
        protected abstract void Init();
        protected virtual void OnUpdate() { }
    }
}
