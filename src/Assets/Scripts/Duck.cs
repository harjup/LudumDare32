﻿using System;
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
            Mugging,
            Other
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
                    CurrentState = State.Other;
                    break;
                case State.Other:
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

        private float diveTargetX = -8.22f;
        private GameObject targetBread;
        void Dive()
        {
            var camOriginalPosition = Camera.main.transform.position;
            var originalXPosition = transform.position.x;
            _rigidbody2D.velocity = Vector2.zero;

            Sequence diveSequence = DOTween.Sequence();
            diveSequence.Append(transform.DOMove(new Vector3(diveTargetX, DiveDepth, 0f), 1.0f).SetEase(Ease.InOutCubic));
            diveSequence.AppendCallback(() => CanMug = true);
            diveSequence.Append(Camera.main.DOShakePosition(.2f, .5f));
            diveSequence.Append(Camera.main.transform.DOMove(camOriginalPosition, .1f));
            diveSequence.AppendCallback(() => Destroy(targetBread));
            diveSequence.AppendCallback(() => CanMug = false);
            diveSequence.Append(transform.DOMove(new Vector3(-8.22f, 1f, 0), .5f).SetEase(Ease.Linear));
            diveSequence.Append(transform.DOMove(new Vector3(originalXPosition, OriginalHeight, 0f), .3f).SetEase(Ease.Linear));
            diveSequence.AppendCallback(() => CurrentState = State.Flying);
        }

        // Returns a bool so this method can get called in a Select
        // Do doesn't exist in regular linq
        public bool PursueBread(GameObject target)
        {
            diveTargetX = target.transform.position.x;
            targetBread = target;
            CurrentState = State.Diving;
            return true;
        }
    }
}

