using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Civilian : MonoBehaviourBase
    {
        public float MoneyHeld;
        public float MinMoveSpeed;
        public float MaxMoveSpeed;
        public State CurrentState;
        public bool Stopped;

        private Rigidbody2D _rigidbody2D;
        public float MoveSpeed;
        private Animator _animator;

        public enum State
        {
            Wandering,
            Mugged,
            Fleeing
        }

        // Use this for initialization
        void Start ()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            SetMoveSpeed();
            _animator = GetComponent<Animator>();

            CurrentState = State.Wandering;

            StartCoroutine(Wander(Random.Range(0, 10), Random.Range(0, 13)));
        }

        private void SetMoveSpeed()
        {
            MoveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);

            if (Math.Abs(MoveSpeed) < 1)
            {
                SetMoveSpeed();
            }
        }

        // Update is called once per frame
        void Update ()
        {
            switch (CurrentState)
            {
                case State.Wandering:
                    if (!Stopped)
                    {
                        _rigidbody2D.velocity = new Vector2(MoveSpeed, 0);

                        // Change animation speed based on movement speed
                        // Note: animation speed cannot be negative
                        _animator.speed = Math.Abs(MoveSpeed);
                    }
                    else
                    {
                        _animator.speed = 0;
                    }
                    break;
                case State.Mugged:
                    //TODO: placeholder for mugged animation state
                    transform.localEulerAngles = new Vector3(0,0,-90);

                    gameObject.layer = LayerMask.NameToLayer("Mugged Civilians");
                    _rigidbody2D.isKinematic = true;
                    break;
            }
        }

        public void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Screen Bounds")
            {
                print("hit wall");
                TurnAround();
            }
        }

        private IEnumerator Wander(float secondsToWait, float stopSeed)
        {
            if (CurrentState == State.Wandering)
            {
                Stopped = (stopSeed > 10);
                yield return new WaitForSeconds(secondsToWait);
                TurnAround();
                StartCoroutine(Wander(Random.Range(0, 10), Random.Range(0, 13)));
            }
        }

        private void TurnAround()
        {
            MoveSpeed *= -1;
        }

        public void CleanUp()
        {
            //TODO: Possibly add some extra special animation before destroying self
            Destroy(gameObject);
        }
    }
}
