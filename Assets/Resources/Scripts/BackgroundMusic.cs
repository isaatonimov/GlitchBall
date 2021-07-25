using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

namespace GlitchBallVR
{
    public class BackgroundMusic : MonoBehaviour
    {
        public SoundFXRef Track;
        // Start is called before the first frame update
        void Start()
        {
            Track.PlaySound();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

