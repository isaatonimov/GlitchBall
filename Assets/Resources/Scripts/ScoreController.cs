using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GlitchBallVR
{
    public class ScoreController : MonoBehaviour
    {
        private int score = 0;
        private TextMeshProUGUI scoreText;
        // Start is called before the first frame update
        void Start()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowCurrentScore()
        {
            if (score < 0)
            {
                scoreText.SetText("GMVER");
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

        public void AddScorePoints(int points)
        {
            Score += points;
        }

        public void SubstractScorePoints(int points)
        {
            Score -= points;
        }

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;

                ShowCurrentScore();
            }
        }
    }

}