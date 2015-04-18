using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Duck : MonoBehaviourBase
    {
        private Rigidbody2D _rigidbody2D;
        public float MoveSpeed;
        private Animator _animator;
        public State CurrentState;

        public enum State
        {
            Flying,
            Diving,
            Mugging
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}

