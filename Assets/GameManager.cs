using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OVR;

namespace GlitchBallVR
{
    public class GameManager : MonoBehaviour
    {
        public SoundFXRef RoundStart;
        public SoundFXRef StartCounter;

        public float      TimerDurationPerTick  = 1f;
        public float      TimeBeforeRoundStarts   = 3f;

        public UnityEvent GameLoaded;
        public UnityEvent NewRound;
        public UnityEvent GameOver;
        public UnityEvent GameWin;
        public UnityEvent CounterTick;

        public MessageController MsgController;

        public List<UnityEvent>     Rounds;
        public List<int>            ScoreToNextRound;

        private int                 scoreToNextRoundIndex = 0;
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

        public void ShowCurrentRound()
        {
            MsgController.SetNewHUDText("ROUND " + currentRound);
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
            StartCoroutine(StartLevelWithCounter(round));
        }

        IEnumerator StartLevelWithCounter(int round)
        {
            NewRound.Invoke();

            yield return new WaitForSeconds(TimeBeforeRoundStarts);

            for (int i = 0; i < 4; i++)
            {
                CounterTick.Invoke();
                StartCounter.PlaySound();
                yield return new WaitForSeconds(TimerDurationPerTick);
            }
            CounterTick.Invoke();
            RoundStart.PlaySound();

            Rounds[round].Invoke();
        }

        public int CurrentScore
        {
            get
            {
                return currentScore;
            }
            set
            {
                if (currentScore < 0)
                {
                    GameOver.Invoke();
                }

                if (currentScore == ScoreToNextRound[scoreToNextRoundIndex])
                {
                    if(scoreToNextRoundIndex + 1 <= Rounds.Count)
                    {
                        currentRound++;
                        StartCoroutine(StartLevelWithCounter(scoreToNextRoundIndex + 1));
                        scoreToNextRoundIndex++;
                    }
                    else
                    {
                        GameWin.Invoke();
                    }
                }

                currentScore = value;
            }
        }


    }
}

