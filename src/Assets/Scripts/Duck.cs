using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class Duck : MonoBehaviourBase
    {
        private Rigidbody2D _rigidbody2D;
        private float _diveTargetX = -8.22f;
        private GameObject _targetBread;

        public float MoveSpeed;
        private Animator _animator;
        public State CurrentState;
        public float OriginalHeight;
        public float DiveDepth;
        public bool CanMug;
        public GameObject BreadPieceTarget;
        public bool CanReceiveCommands;

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

            DuckManager.Instance.DuckList.Add(this);
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
                other.gameObject.GetComponent<Civilian>().GetMugged();
            }
        }

        private void TurnAround()
        {
            MoveSpeed *= -1;
            transform.localScale = transform.localScale.SetX(transform.localScale.x*-1);
        }

        void Dive()
        {
            var camOriginalPosition = Camera.main.transform.position;
            var originalXPosition = transform.position.x;
            _rigidbody2D.velocity = Vector2.zero;

            var diveSequence = DOTween.Sequence();
            diveSequence.AppendCallback(() => _animator.SetTrigger("Dive"));
            diveSequence.AppendCallback(() => CanReceiveCommands = false);
            diveSequence.AppendCallback(() => CanMug = true);
            diveSequence.Append(transform.DOMove(new Vector3(_diveTargetX, DiveDepth, 0f), 1.0f).SetEase(Ease.InOutCubic));
            diveSequence.Append(Camera.main.DOShakePosition(.2f, .5f));
            diveSequence.Append(Camera.main.transform.DOMove(camOriginalPosition, .1f));
            diveSequence.AppendCallback(() => Destroy(_targetBread));
            diveSequence.AppendCallback(() => CanMug = false);
            diveSequence.AppendCallback(() => _animator.SetTrigger("Default"));
            diveSequence.AppendCallback(() => FindObjectOfType<BreadThrow>().GetTheMoney());
            diveSequence.Append(transform.DOMove(new Vector3(-8.22f, 1f, 0), .5f).SetEase(Ease.Linear));
            diveSequence.Append(transform.DOMove(new Vector3(originalXPosition, OriginalHeight, 0f), .3f).SetEase(Ease.Linear));
            diveSequence.AppendCallback(() => CurrentState = State.Flying);
            diveSequence.AppendCallback(() => CanReceiveCommands = true);
            diveSequence.AppendCallback(() => DuckManager.Instance.DuckList.Insert(DuckManager.Instance.DuckList.Count, this));
        }

        // Returns a bool so this method can get called in a Select
        // Do doesn't exist in regular linq
        public bool PursueBread(GameObject target)
        {
            _diveTargetX = target.transform.position.x;
            _targetBread = target;
            CurrentState = State.Diving;
            return true;
        }
    }
}

