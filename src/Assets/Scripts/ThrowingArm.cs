using System;
using UnityEngine;
using System.Collections;

public class ThrowingArm : MonoBehaviourBase
{

    private GameObject _armGraphic;
    private Vector2 _currentVelocity;
    public Vector2 _previousMousePos;
    private LargeBread _largeBread;
    private BreadBasket _breadBasket;


    void Start()
    {
        _armGraphic = transform.FindChild("ArmGraphic").gameObject;
        _largeBread = FindObjectOfType<LargeBread>();
        _breadBasket = FindObjectOfType<BreadBasket>();
        _currentVelocity = Vector2.zero;
        _previousMousePos = Vector2.zero;

        _breadBasket.Hide();
        _largeBread.Hide();
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        var deltaMouse = (mousePos - _previousMousePos);

        deltaMouse = new Vector2(deltaMouse.x/Screen.width, deltaMouse.y/Screen.height) / Time.smoothDeltaTime;


        Debug.Log(deltaMouse);
        _previousMousePos = mousePos;
        
        var targetPos = Camera.main.ScreenToWorldPoint(mousePos).SetZ(-5);

        if (Input.GetButtonDown("Fire1"))
        {
            //Initialize throwing arm
            transform.position = targetPos;
            // Lets have some hardcoded limits so the player can always reach the basket
            if (transform.position.x > 5)
            {
                transform.position = transform.position.SetX(5);
            }
            if (transform.position.y < -1)
            {
                transform.position = transform.position.SetY(-1);
            }

            _armGraphic.SetActive(true);
            _breadBasket.Show();
        }

        if (Input.GetButton("Fire1"))
        {
            //TODO: What a horrible block of maths! Clean up eventually? Put in own function?
            // Start maths
            var heading = (targetPos - transform.position);
            var distance = heading.magnitude;

            _armGraphic.transform.localScale = Vector3.one;
            if (Math.Abs(distance) >= .001f)
            {
                var direction = heading / distance;
                var angle = Vector2.Angle(direction, Vector2.right);
                Vector3 cross = Vector3.Cross(direction, Vector2.right);
                if (cross.z > 0)
                {
                    angle = 360 - angle;
                }

                _armGraphic.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                _armGraphic.transform.localScale = _armGraphic.transform.localScale.SetX(distance) * 4.5f;

                _currentVelocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            }

            // End Maths

            // Throwing update
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<BreadBasket>())
                {
                    _breadBasket.Hide();
                    _largeBread.Show();
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (_largeBread.GetComponent<SpriteRenderer>().enabled)
            {
                // Throwing end, actually throw bread
                // Now we need a final direction & magnitude
                var targetVel = FindObjectOfType<LargeBread>().GetComponent<Rigidbody2D>().velocity;
                FindObjectOfType<BreadThrow>().ThrowTheBread(targetVel / 5f);
            }

            _largeBread.Hide();
            _armGraphic.SetActive(false);
        }
    }
}
