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
        public List<ShootingMode> ShootingPattern;

        //public Transform          Magazine;
        public GameObject ProjectilePrefab;
        public GameObject TrapPrefab;

        public bool TrapsEnabled = true;
        //Chance of insatiating Trap instead of Normal Projectile 0-100%
        [Range(0f, 100f)]
        public float TrapChance = 30f;

        public bool RandomStops;
        public bool StopAtWaypoint = true;
        [Range(0f, 10f)]
        public float MinStopDuration = 2f;
        [Range(0f, 10f)]
        public float MaxStopDuration = 4f;
        [Range(0f, 10f)]
        public float MovementSpeed = 3f;

        public bool RotateOnStop = true;
        [Range(0f, 90f)]
        public float RotationXmin = 15f;
        [Range(0f, 90f)]
        public float RotationXmax = 45f;
        [Range(5f, 10f)]
        public float ShootingForceMin = 8.25f;
        [Range(5f, 10f)]
        public float ShootingForceMax = 8.5f;

        public bool ShootTimerActive = false;
        public bool ShootAtWaypoint = true;
        [Range(1f, 10f)]
        public float ShootingTimerInterval = 1f;

        //public bool BurstMode = false;
        public int Burstrate = 4;
        [Range(0f, 1f)]
        public float BurstInterval = 0.2f;

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
                float step = MovementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, currentWaypointTarget.transform.position, step);

                //when waypoint is reached, go to next waypoint
                if (Vector3.Distance(transform.position, currentWaypointTarget.transform.position) < 0.001f)
                {
                    if (StopAtWaypoint)
                    {
                        StartCoroutine(WaitAndRotate(Random.Range(MinStopDuration, MaxStopDuration)));

                        if (ShootAtWaypoint)
                        {
                            ShootAccordingToPattern();
                        }
                    }

                    int currentIndex = Waypoints.IndexOf(currentWaypointTarget);
                    currentWaypointTarget = Waypoints[Random.Range(0, Waypoints.Count)];
                }

                if (ShootTimerActive)
                {
                    if (shootTimer > 0)
                    {
                        shootTimer -= Time.deltaTime;
                    }
                    else
                    {
                        shootTimer = Random.Range(1f, ShootingTimerInterval);
                        ShootAccordingToPattern();
                    }
                }

                //Code for testing cannon balls - buggy - needs timer
                //if (Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0 || Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0)
                //{
                //    Shoot();
                //}

            }
        }

        //public void LoadNewConfiguration(GameObject gameObject)
        //{
        //    if (gameObject.GetComponent<RoundConfiguration>() != null)
        //        StartWithConfiguration(gameObject.GetComponent<RoundConfiguration>());
        //    else
        //        StartWithoutConfiguration();
        //}

        public void StartWithConfiguration(RoundConfiguration roundConfig)
        {
            ShootingPattern         = roundConfig.ShootingPattern;
            TrapsEnabled            = roundConfig.TrapsEnabled;
            RandomStops             = roundConfig.RandomStops;
            StopAtWaypoint          = roundConfig.StopAtWaypoint;
            MinStopDuration         = roundConfig.MinStopDuration;
            MaxStopDuration         = roundConfig.MaxStopDuration;
            MovementSpeed           = roundConfig.MovementSpeed;
            RotateOnStop            = roundConfig.RotateOnStop;
            RotationXmin            = roundConfig.RotationXmin;
            RotationXmax            = roundConfig.RotationXmax;
            ShootingForceMin        = roundConfig.ShootingForceMin;
            ShootTimerActive        = roundConfig.ShootTimerActive;
            ShootAtWaypoint         = roundConfig.ShootAtWaypoint;
            ShootingTimerInterval   = roundConfig.ShootingTimerInterval;
            Burstrate               = roundConfig.Burstrate;
            BurstInterval           = roundConfig.BurstInverval;

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
            Quaternion targetRotation = Quaternion.Euler(Random.Range(RotationXmin, RotationXmax), transform.rotation.y, transform.rotation.z);
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
            //Debug.Log("pattern index (before shooting): " + currentShootModeIndex);

            Shoot(ShootingPattern[currentShootModeIndex], Burstrate, BurstInterval);



            if (currentShootModeIndex == ShootingPattern.Count - 1)
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

            if (Random.Range(0f, 100f) > TrapChance)
                projectileToShoot = GameObject.Instantiate(ProjectilePrefab);
            else
                projectileToShoot = GameObject.Instantiate(TrapPrefab);

            projectileToShoot.transform.position = transform.GetChild(0).position;

            //projectileToShoot.gameObject.SetActive(true);

            float randomShootingForce = Random.Range(ShootingForceMin, ShootingForceMax);

            projectileToShoot.GetComponent<Rigidbody>().AddForce(new Vector3(0, randomShootingForce * 2, -randomShootingForce / 2), ForceMode.Impulse);

            //-> for calculating different angle - Mathf.Tan(currentRotation.x);
        }

    }

}
