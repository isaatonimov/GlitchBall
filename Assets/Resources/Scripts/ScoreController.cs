using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GlitchBallVR
{
    public class ScoreController : MonoBehaviour
    {
        private TextMeshProUGUI scoreText;
        // Start is called before the first frame update
        void Start()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }

        public void ShowCurrentScore(int score)
        {
            if (score <= 0)
            {
                scoreText.SetText("00");
            }

            if (score < 10 && score >= 0)
            {
                scoreText.SetText("0" + score);
            }
            else if (score > 0)
            {
                scoreText.SetText(score.ToString());
            }
        }

    }

}