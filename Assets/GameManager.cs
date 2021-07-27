using System.Collections;
using System.Collections.Generic;
using OVR;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


namespace GlitchBallVR
{
    public class GameManager : MonoBehaviour
    {
        public SoundFXRef RoundStart;
        public SoundFXRef StartCounter;

        public ScoreController ScoreBoard;
        public TextMeshProUGUI GameOverScore;
        public RoundConfiguration currentRoundConfig;

        public float      TimerDurationPerTick  = 1f;
        public float      TimeBeforeRoundStarts   = 3f;

        public UnityEvent GameLoaded;
        public UnityEvent NewRound;
        public UnityEvent GameOver;
        public UnityEvent GameWin;
        public UnityEvent CounterTick;

        public MessageController MsgController;

        public List<UnityEvent>     Rounds;

        private int                 currentLifes = 3;
        private int                 currentRound = 0;

        private int currentScore;
        // Start is called before the first frame update
        void Start()
        {
            if(Rounds == null)
                Rounds = new List<UnityEvent>();

            GameLoaded.Invoke();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetCurrentRoundConfig(RoundConfiguration roundConfig)
        {
            currentRoundConfig = roundConfig;
        }

        public void ShowCurrentRound()
        {
            RoundStart.PlaySound();
            MsgController.SetNewHUDText("ROUND " + currentRoundConfig.Round);
        }

        public void DeleteAllCurrentProjectiles()
        {
            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");

            foreach (GameObject gobj in projectiles)
            {
                GameObject.Destroy(gobj);
            }

            foreach (GameObject gobj in traps)
            {
                GameObject.Destroy(gobj);
            }
        }

        public void RemoveLife()
        {
            CurrentLifes--;
        }
        public void AddScorePoints(int points)
        {
            CurrentScore += points;
        }

        public void SubstractScorePoints(int points)
        {
            CurrentScore -= points;
        }

        public void StartLevel(int round)
        {
            if (round == 0)
                CurrentScore = 0;

            StartCoroutine(StartLevelWithCounter(round));
        }

        IEnumerator StartLevelWithCounter(int round)
        {
            NewRound.Invoke();

            yield return new WaitForSeconds(TimeBeforeRoundStarts);

            for (int i = 0; i < 3; i++)
            {
                CounterTick.Invoke();
                StartCounter.PlaySound();
                yield return new WaitForSeconds(TimerDurationPerTick);
            }
            CounterTick.Invoke();

            StartCounter.PlaySoundAt(this.transform.position, 0f, 1f, 1.5f);

            Rounds[round].Invoke();
        }

        public void SetGameOverScore()
        {
            GameOverScore.SetText(CurrentScore.ToString());
        }

        public int CurrentLifes 
        {   
            get
            {
                return currentLifes;
            }
            set
            {
                if(currentLifes == 0)
                {
                    GameOver.Invoke();

                    currentLifes = 3;
                }
                else
                {
                    if(currentLifes == 1)
                        MsgController.SetNewHUDText(currentLifes + " life left");
                    else
                        MsgController.SetNewHUDText(currentLifes + " lifes left");

                    currentLifes = value;
                }
            }
        }
        public int CurrentScore
        {
            get
            {
                return currentScore;
            }
            set
            {

                if (currentScore == currentRoundConfig.ScoreToNextRound)
                {
                    if (currentRoundConfig.Round + 2 > Rounds.Count)
                        GameWin.Invoke();
                    else
                    {
                        Rounds[currentRoundConfig.Round + 1].Invoke();
                        StartLevel(currentRoundConfig.Round);
                    }
                }

                if(value < 0)
                {
                    ScoreBoard.ShowCurrentScore(0);
                }
                else
                {
                    ScoreBoard.ShowCurrentScore(value);
                    currentScore = value;
                }

            }
        }


    }
}

