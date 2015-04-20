using System;
using System.Collections;
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
        private IEnumerator _wanderController;

        private Rigidbody2D _rigidbody2D;
        public float MoveSpeed;
        private Animator _animator;
        private bool _canTurn = true;
        

        public enum State
        {
            Walking,
            Mugged,
            Spooked,
            Fleeing
        }

        void Awake ()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _wanderController = WanderController(Random.Range(0f, 5f), Random.Range(0, 13));
        }

        // Use this for initialization
        void Start ()
        {
            SetMoveSpeed();
            CurrentState = State.Walking;
            StartCoroutine(_wanderController);
            MoneyHeld = 20;
        }

        private void SetMoveSpeed ()
        {
            MoveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);

            if (Math.Abs(MoveSpeed) > 1 && MoveSpeed > 0)
                transform.localScale = transform.localScale.SetX(transform.localScale.x*-1);

            else if (Math.Abs(MoveSpeed) < 1)
                SetMoveSpeed();
        }

        // Update is called once per frame
        void Update ()
        {
            switch (CurrentState)
            {
                case State.Walking:
                    if (!Stopped)
                    {
                        _rigidbody2D.velocity = new Vector2(MoveSpeed, 0);
                        _animator.speed = Math.Abs(MoveSpeed);
                    }
                    else
                        _animator.speed = 0;

                    break;

                case State.Mugged:
                    gameObject.layer = LayerMask.NameToLayer("Mugged Civilians");
                    _rigidbody2D.isKinematic = true;
                    break;

                case State.Spooked:
                    _rigidbody2D.velocity = new Vector2(MoveSpeed * 1.5f, 0);
                    break;

                case State.Fleeing:
                    _canTurn = false;
                    StopCoroutine(_wanderController);
                    gameObject.layer = LayerMask.NameToLayer("Fleeing Civilians");
                    _rigidbody2D.velocity = new Vector2(MoveSpeed * 2f, 0);
                    break;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Screen Bounds")
            {
                if (CurrentState == State.Fleeing)
                {
                    Debug.Log("Leaving");
                    Leave();
                }
            }
        }
        
        
        public void OnCollisionEnter2D (Collision2D coll)
        {
            if (coll.gameObject.tag == "Screen Bounds" && CurrentState != State.Fleeing)
            {
                TurnAround();    
            }

            // Probably don't need the first condition
            if (CurrentState != State.Mugged && 
                coll.gameObject.tag == "Civilian" && 
                coll.gameObject.GetComponent<Civilian>().CurrentState == State.Mugged)
            {
                if (CurrentState == State.Spooked)
                {
                    CurrentState = State.Fleeing;
                    TurnAround();
                    return;
                }

                CurrentState = State.Spooked;
                TurnAround();
            }
        }

        private IEnumerator WanderController (float secondsToWait, float stopSeed)
        {
            Stopped = (stopSeed > 10);
            yield return new WaitForSeconds(secondsToWait);
            TurnAround();
            StartCoroutine(_wanderController);
        }

        private void TurnAround ()
        {
            if (!_canTurn) return;
            MoveSpeed *= -1;
            transform.localScale = transform.localScale.SetX(transform.localScale.x * -1);
        }

        public void GetMugged()
        {
            CurrentState = State.Mugged;
            Stopped = false;
            _animator.speed = 1f;
            
            _animator.SetTrigger("Mugged");
            CivilianManager.Instance.RemoveCivilianFromActiveCount(gameObject);
            MoneyHeld = 0;
        }

        public void Leave()
        {
            CivilianManager.Instance.RemoveCivilianFromActiveCount(gameObject);
        }

        public void CleanUp ()
        {
            //TODO: Possibly add some extra special animation before destroying self
            Destroy(gameObject);
        }
    }
}
