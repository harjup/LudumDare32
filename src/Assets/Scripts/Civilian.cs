using UnityEngine;

namespace Assets.Scripts
{
    public class Civilian : MonoBehaviour
    {
        public float MoneyHeld;
        public float MoveSpeed;

        private Rigidbody2D _rigidbody2D;

        // Use this for initialization
        void Start ()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = new Vector2(MoveSpeed, 0);
        }
	
        // Update is called once per frame
        void Update ()
        {
            _rigidbody2D.velocity = new Vector2(MoveSpeed, 0);
        }
    }
}
