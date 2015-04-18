using System;
using UnityEngine;
using System.Collections;

public class ThrowingArm : MonoBehaviourBase
{

    private GameObject _armGraphic;
    private Vector2 _currentVelocity;
    private Vector2 _previousMousePos;

    void Start()
    {
        _armGraphic = transform.FindChild("ArmGraphic").gameObject;
        _currentVelocity = Vector2.zero;
        _previousMousePos = Vector2.zero;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        var deltaMouse = (mousePos - _previousMousePos);


        deltaMouse = new Vector2(deltaMouse.x/Screen.width, deltaMouse.y/Screen.height) / Time.smoothDeltaTime;




        Debug.Log(deltaMouse);
        _previousMousePos = mousePos;
        
        var targetPos = Camera.main.ScreenToWorldPoint(mousePos).SetZ(0);

        



        if (Input.GetButtonDown("Fire1"))
        {
            //Initialize throwing arm
            transform.position = targetPos;
            _armGraphic.SetActive(true);
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
                _armGraphic.transform.localScale = _armGraphic.transform.localScale.SetX(distance) * 2;

                _currentVelocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            }

            // End Maths

            // Throwing update
        }

        if (Input.GetButtonUp("Fire1"))
        {
            // Throwing end, actually throw bread
            // Now we need a final direction & magnitude
            FindObjectOfType<BreadThrow>().ThrowTheBread(deltaMouse);

            _armGraphic.SetActive(false);
        }
    }
}
