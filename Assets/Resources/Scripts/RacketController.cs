using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OVR;

namespace GlitchBallVR
{
    public class RacketController : MonoBehaviour
    {
        public UnityEvent HitProjectile;
        public UnityEvent HitTrap;

        public SoundFXRef HitSound;
        public SoundFXRef HitTrapSound;

        // Start is called before the first frame update
        void Start()
        {
            if (HitProjectile == null)
                HitProjectile = new UnityEvent();

            if (HitTrap == null)
                HitTrap = new UnityEvent();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Projectile")
            {
                HitProjectile.Invoke();
                HitSound.PlaySound();
                //collision.gameObject.tag = "HitProjectile";
                GameObject.Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "Trap")
            {
                HitTrap.Invoke();
                HitTrapSound.PlaySound();
                GameObject.Destroy(collision.gameObject);
            }
        }
    }

}
