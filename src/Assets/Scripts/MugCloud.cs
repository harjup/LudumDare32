using UnityEngine;

namespace Assets.Scripts
{
    public class MugCloud : MonoBehaviourBase
    {
        public float MugMoney;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Civilian")
            {
                var muggedCiv = other.gameObject.GetComponent<Civilian>();
                MugMoney += muggedCiv.MoneyHeld;
                muggedCiv.GetMugged();
            }
        }
    }
}