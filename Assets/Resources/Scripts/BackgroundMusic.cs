using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

namespace GlitchBallVR
{
    public class BackgroundMusic : MonoBehaviour
    {
        public SoundFXRef Track;
        private bool trackPlaying;

        // Start is called before the first frame update
        void Start()
        {
            Track.PlaySound();
            trackPlaying = true;
        }

        public void ToggleBackgroundTrack()
        {
            if (trackPlaying)
            {
                Track.StopSound();
                trackPlaying = false;
            }
            else
            {
                Track.PlaySound();
                trackPlaying = true;
            }
        }
    }
}

