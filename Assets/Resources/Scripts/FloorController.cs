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

        private bool behaviourPaused = false;

        // Start is called before the first frame update
        void Start()
        {
            if (Miss == null)
                Miss = new UnityEvent();
        }

        // Update is called once per frame
        void Update()
        {

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

                MissSound.PlaySound();
                GameObject.Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "Trap" && behaviourPaused == false)
            {
                GameObject.Destroy(collision.gameObject);
            }

        }
    }

}
