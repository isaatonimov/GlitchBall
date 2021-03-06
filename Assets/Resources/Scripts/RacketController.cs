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

        public bool IsLeftController;
        public SoundFXRef HitSound;
        public SoundFXRef HitTrapSound;
        public GameObject ParticlesOnDestroy;

        // Start is called before the first frame update
        void Start()
        {
            if (HitProjectile == null)
                HitProjectile = new UnityEvent();

            if (HitTrap == null)
                HitTrap = new UnityEvent();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Projectile")
            {
                
                HitProjectile.Invoke();
                HitSound.PlaySoundAt(collision.transform.position);

                GameObject particles = GameObject.Instantiate(ParticlesOnDestroy);
                particles.transform.position = collision.transform.position;

                GameObject.Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "Trap")
            {
                HitTrap.Invoke();
                HitTrapSound.PlaySoundAt(collision.transform.position);

                OVRInput.SetControllerVibration(0.1f, 0.2f, OVRInput.Controller.LTouch);
                OVRInput.SetControllerVibration(0.1f, 0.2f, OVRInput.Controller.RTouch);


                GameObject particles = GameObject.Instantiate(ParticlesOnDestroy);
                particles.transform.position = collision.transform.position;

                GameObject.Destroy(collision.gameObject);
            }
        }
    }

}
