using UnityEngine;

namespace Assets.Scripts
{
    public class MugCloud : MonoBehaviourBase
    {
        public float MugMoney;
        public MumblePlayer MumblePlayer;

        public void Start()
        {
            var mumblePrefab = Resources.Load<GameObject>(@"Prefabs/Sounds/MumblePlayer") as GameObject;
            var mumblePlayerInstance = Instantiate(mumblePrefab);
            mumblePlayerInstance.transform.SetParent(transform);
            MumblePlayer = mumblePlayerInstance.GetComponent<MumblePlayer>();
            MumblePlayer.PlayMumble(MumblePlayer.MumbleType.Fight);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Civilian")
            {
                var muggedCiv = other.gameObject.GetComponent<Civilian>();
                MugMoney += muggedCiv.MoneyHeld;
                muggedCiv.GetMugged();
            }
        }

        void OnDestroy()
        {
            MumblePlayer.StopMumble();
        }


    }
}