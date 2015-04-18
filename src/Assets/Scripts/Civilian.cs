using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Civilian : MonoBehaviourBase
    {
        public float MoneyHeld;
        public float MinMoveSpeed;
        public float MaxMoveSpeed;
        public State CurrentState;
        public bool Walking;

        private Rigidbody2D _rigidbody2D;
        private float _moveSpeed;

        public enum State
        {
            Wandering,
            Fleeing
        }

        // Use this for initialization
        void Start ()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _moveSpeed = Random.Range(MinMoveSpeed, MaxMoveSpeed);
            CurrentState = State.Wandering;

            StartCoroutine(Wander(Random.Range(0, 10), Random.Range(0, 13)));
        }
	
        // Update is called once per frame
        void Update ()
        {
            if (Walking)
            {
                _rigidbody2D.velocity = new Vector2(_moveSpeed, 0);
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

        private IEnumerator Wander(float secondsToWait, float stateSeed)
        {
            Walking = (stateSeed < 10);


            if (CurrentState == State.Wandering)
            {
                yield return new WaitForSeconds(secondsToWait);
                TurnAround();
                StartCoroutine(Wander(Random.Range(0, 10), Random.Range(0, 13)));
            }
        }

        private void TurnAround()
        {
            _moveSpeed *= -1;
        }

        public void CleanUp()
        {
            //TODO: Possibly add some extra special animation before destroying self
            Destroy(gameObject);
        }
    }
}
