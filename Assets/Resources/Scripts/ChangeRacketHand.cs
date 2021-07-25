using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

namespace GlitchBallVR
{
    public class ChangeRacketHand : MonoBehaviour
    {
        public SoundFXRef RacketChangeSound;
        public Transform LeftHandAnchor;
        public Transform RightHandAnchor;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") > 0)
            {

                RacketChangeSound.PlaySoundAt(LeftHandAnchor.GetChild(0).position);
                LeftHandAnchor.GetChild(0).gameObject.SetActive(true);
                RightHandAnchor.GetChild(0).gameObject.SetActive(false);
            }

            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") > 0)
            {
                RacketChangeSound.PlaySoundAt(RightHandAnchor.GetChild(0).position);
                RightHandAnchor.GetChild(0).gameObject.SetActive(true);
                LeftHandAnchor.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}

