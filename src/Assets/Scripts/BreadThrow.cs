using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BreadThrow : MonoBehaviourBase
{
    private GameObject _breadPiece;
    private Vector3 _throwPosition;
    private Animator _animator;
    private ParticleSystem _moneyGet;
    private AudioSource _moneySound;
    public void Start()
    {
        _breadPiece = Resources.Load<GameObject>("Prefabs/Actors/BreadPiece");
        _throwPosition = transform.position;
        _animator = GetComponent<Animator>();
        _moneyGet = transform.GetChild(0).GetComponent<ParticleSystem>();
        _moneySound = GetComponent<AudioSource>();
    }


    public void ThrowTheBread(Vector2 force)
    {
        var bread = Instantiate(_breadPiece, _throwPosition, transform.rotation) as GameObject;
        var breadRigidbody = bread.GetComponent<Rigidbody2D>();

        breadRigidbody.velocity = force;
        Debug.Log(breadRigidbody.velocity);

        _animator.SetTrigger("Throw");
    }

    public void GetTheMoney()
    {
        _animator.SetTrigger("Success");
    }

    public void PlayTheMoneySoundAndEffect()
    {
        Debug.Log("PlayTheMoneySoundAndEffect");
        _moneyGet.Stop();
        _moneyGet.Clear();
        _moneyGet.Play();
        // play the sound
        _moneySound.Play();

    }
}
