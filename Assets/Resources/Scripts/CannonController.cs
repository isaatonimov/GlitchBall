using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;


namespace GlitchBallVR
{
    public enum ShootingMode
    {
        OneShot,
        Burst,
        FastBurst
    }
    public class CannonController : MonoBehaviour
    {
        public SoundFXRef CannonShoot;

        public List<GameObject> Waypoints;

        public GameObject ProjectilePrefab;
        public GameObject TrapPrefab;

        public Transform fixedCannonShootingPoint;
        public Transform player;

        private RoundConfiguration currentRoundConfig;

        private GameObject currentWaypointTarget;
        private int currentShootModeIndex = 0;

        private bool currentlyStopped       = true;
        private bool currentlyWaitingWPnt   = false;
        private bool currentlyRotating      = false;
        private bool currentlyShooting      = false;

        private Quaternion currentRotation;
        private float shootTimer = 5f;

        // Start is called before the first frame update
        void Start()
        {
            currentWaypointTarget = Waypoints[0];
            currentRotation = transform.rotation;
        }
        // Update is called once per frame
        void Update()
        {
            if (currentlyStopped == false)
            {
                float step = currentRoundConfig.MovementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, currentWaypointTarget.transform.position, step);

                //when waypoint is reached, go to next waypoint
                if (Vector3.Distance(transform.position, currentWaypointTarget.transform.position) < 0.001f)
                {
                    if (currentRoundConfig.StopAtWaypoint)
                    {
                        StartCoroutine(WaitAndRotate(Random.Range(currentRoundConfig.MinStopDuration, currentRoundConfig.MaxStopDuration)));

                        if (currentRoundConfig.ShootAtWaypoint)
                        {
                            ShootAccordingToPattern();
                        }
                    }

                    int currentIndex = Waypoints.IndexOf(currentWaypointTarget);
                    currentWaypointTarget = Waypoints[Random.Range(0, Waypoints.Count)];
                }

                if (currentRoundConfig.ShootTimerActive)
                {
                    if (shootTimer > 0)
                    {
                        shootTimer -= Time.deltaTime;
                    }
                    else
                    {
                        shootTimer = Random.Range(1f, currentRoundConfig.ShootingTimerInterval);
                        ShootAccordingToPattern();
                    }
                }
            }
        }

        public void StartWithConfiguration(RoundConfiguration roundConfig)
        {
            currentRoundConfig = roundConfig;

            currentlyStopped = false;
        }

        public void StartWithoutConfiguration()
        {
            currentlyStopped = false;
        }

        public void Stop()
        {
            currentlyStopped = true;
            StopAllCoroutines();
        }


        IEnumerator Rotate()
        {
            float speed = 3f;
            Quaternion targetRotation = Quaternion.Euler(Random.Range(currentRoundConfig.RotationXmin, currentRoundConfig.RotationXmax), transform.rotation.y, transform.rotation.z);
            currentRotation = targetRotation;

            while (transform.rotation != targetRotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);

                yield return null;
            }
        }

        IEnumerator Wait(float waitTime)
        {
            currentlyStopped = true;
            yield return new WaitForSeconds(waitTime);
            currentlyStopped = false;
        }

        IEnumerator WaitAndRotate(float waitTime)
        {
            StartCoroutine(Wait(waitTime));

            yield return null;
        }

        IEnumerator Burst(int burstRate, float burstInterval)
        {
            for (int i = 0; i < burstRate; i++)
            {
                Shoot();

                yield return new WaitForSeconds(burstInterval);
            }
        }

        private void ShootAccordingToPattern()
        {
            Shoot(currentRoundConfig.ShootingPattern[currentShootModeIndex], currentRoundConfig.Burstrate, currentRoundConfig.BurstInterval);

            if (currentShootModeIndex == currentRoundConfig.ShootingPattern.Count - 1)
                currentShootModeIndex = 0;
            else
                currentShootModeIndex++;
        }


        private void Shoot(ShootingMode shootingMode, int optionalBurstRate = 4, float optionalBurstInterval = 0.2f)
        {
            if (shootingMode == ShootingMode.Burst)
                StartCoroutine(Burst(optionalBurstRate, optionalBurstInterval));

            if (shootingMode == ShootingMode.FastBurst)
                StartCoroutine(Burst(optionalBurstRate * 2, optionalBurstInterval / 2));

            if (shootingMode == ShootingMode.OneShot)
                Shoot();
        }

        private void Shoot()
        {
            CannonShoot.PlaySoundAt(transform.position);

            GameObject projectileToShoot = null;

            if (Random.Range(0f, 100f) > currentRoundConfig.TrapChance)
                projectileToShoot = GameObject.Instantiate(ProjectilePrefab);
            else
                projectileToShoot = GameObject.Instantiate(TrapPrefab);

            projectileToShoot.transform.position = transform.GetChild(0).position;
            //projectileToShoot.transform.position = fixedCannonShootingPoint.position;

            float randomShootingForce = Random.Range(currentRoundConfig.ShootingForceMin, currentRoundConfig.ShootingForceMax);

            var gravity = System.Math.Abs(Physics.gravity.y);
            Debug.Log(gravity);
            Debug.Log(player.position);
            var numSolutions = Ballistics.solve_ballistic_arc(projectileToShoot.transform.position, currentRoundConfig.ShootingForceMin, player.position, gravity, out var s0, out var s1);
            if (numSolutions > 0)
            {
                projectileToShoot.GetComponent<Rigidbody>().AddForce(s0, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("Womp womp");
            }

            //-> for calculating different angle - Mathf.Tan(currentRotation.x);
        }

    }

}
