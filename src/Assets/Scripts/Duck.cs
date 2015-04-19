using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Assets.Scripts
{
    public class Duck : MonoBehaviourBase
    {
        private Rigidbody2D _rigidbody2D;
        public float MoveSpeed;
        private Animator _animator;
        public State CurrentState;
        public float OriginalHeight;
        public float DiveDepth;
        public bool CanMug;
        public GameObject BreadPieceTarget;

        public enum State
        {
            Flying,
            Diving,
            Mugging
        }

        void Start()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            OriginalHeight = transform.position.y;

            CurrentState = State.Flying;
        }

        void Update()
        {
            switch (CurrentState)
            {
                case State.Flying:
                    _rigidbody2D.velocity = new Vector2(MoveSpeed, 0);
                    break;
                case State.Diving:
                    Dive();
                    CanMug = true;
                    CurrentState = State.Flying;
                    break;
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Screen Bounds")
                TurnAround();

            if (other.gameObject.tag == "Civilian" && CanMug)
            {
                other.gameObject.GetComponent<Civilian>().CurrentState = Civilian.State.Mugged;
            }
        }



        private void TurnAround()
        {
            MoveSpeed *= -1;
            transform.localScale = transform.localScale.SetX(transform.localScale.x*-1);
        }

        void Dive()
        {
            Sequence diveSequence = DOTween.Sequence();
            diveSequence.Append(transform.DOMoveY(DiveDepth, 1.5f).SetEase(Ease.InOutCubic));
            diveSequence.Append(transform.DOMove(new Vector3(-8.22f, .664f, 0), 1.5f));
            diveSequence.Append(transform.DOMoveY(OriginalHeight, 1).SetEase(Ease.InOutCubic));
        }
    }
}

