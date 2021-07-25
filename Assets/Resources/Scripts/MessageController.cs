using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GlitchBallVR
{
    public class MessageController : MonoBehaviour
    {
        private Animator animator;
        private TextMeshProUGUI hudText;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            hudText = GetComponent<TextMeshProUGUI>();
        }

        public void SetNewHUDText(string text)
        {
            hudText.SetText(text);
            animator.SetTrigger("Fade_in");
        }
    }
}

