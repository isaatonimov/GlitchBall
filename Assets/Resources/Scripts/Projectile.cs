using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OVR;

namespace GlitchBallVR
{
    public class Projectile : MonoBehaviour
    {
        public SoundFXRef Hit;
        public SoundFXRef Miss;

        public GameObject ParticlesOnDestroy;
        public Transform Magazine;
        public float ResetTime = 15f;

        public UnityEvent HitTarget;
        public UnityEvent MissedTarget;

        private float resetTimer;
        private GameObject thisProjectile;


        // Start is called before the first frame update
        void Start()
        {
            if (HitTarget == null)
                HitTarget = new UnityEvent();

            if (MissedTarget == null)
                MissedTarget = new UnityEvent();

            resetTimer = ResetTime;
            thisProjectile = transform.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.y < -1f)
            {
                //resetPosition();
                GameObject.Destroy(this.gameObject);
            }

            if (resetTimer > 0)
            {
                resetTimer -= Time.deltaTime;
            }
            else
            {
                resetTimer = ResetTime;
                //resetPosition();
                GameObject.Destroy(this.gameObject);
            }

        }

        private void OnDestroy()
        {
            //GameObject particles = GameObject.Instantiate(ParticlesOnDestroy);
            //particles.transform.position = this.transform.position;
        }
        private void OnCollisionEnter(Collision collision)
        {
            //if (collision.transform.tag == "Racket")
            //{
            //    Score.PlaySound();
            //    HitTarget.Invoke();
            //    resetPosition();
            //}
            //else
            //{
            //    MissedTarget.Invoke();
            //    Miss.PlaySound();
            //}
        }

        private void resetPosition()
        {
            transform.SetParent(Magazine);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.Euler(new Vector3(-25, 175, 0));
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            thisProjectile.name = thisProjectile.name.Substring(0, thisProjectile.name.Length - 1);
        }
    }

}
