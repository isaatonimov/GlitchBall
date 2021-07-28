using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OVR;

namespace GlitchBallVR
{
    public class FloorController : MonoBehaviour
    {
        public UnityEvent Miss;
        public SoundFXRef MissSound;
        public GameObject ParticlesOnDestroy;

        private bool behaviourPaused = false;

        // Start is called before the first frame update
        void Start()
        {
            if (Miss == null)
                Miss = new UnityEvent();
        }

        public void PauseBehavior()
        {
            behaviourPaused = true;
        }

        public void ResumBehavior()
        {
            behaviourPaused = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Projectile" )
            {
                if(!behaviourPaused)
                    Miss.Invoke();

                MissSound.PlaySoundAt(collision.transform.position);

                GameObject particles = GameObject.Instantiate(ParticlesOnDestroy);
                particles.transform.position = collision.transform.position;

                //controller vibration on miss
                OVRInput.SetControllerVibration(0.1f, 0.2f, OVRInput.Controller.LTouch);
                OVRInput.SetControllerVibration(0.1f, 0.2f, OVRInput.Controller.RTouch);

                GameObject.Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "Trap" && behaviourPaused == false)
                GameObject.Destroy(collision.gameObject);

        }
    }

}
