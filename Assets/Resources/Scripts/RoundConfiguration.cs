using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlitchBallVR
{
    public class RoundConfiguration : MonoBehaviour
    {

        public List<ShootingMode> ShootingPattern;
        public bool     TrapsEnabled            = false;
        [Range(0f, 100f)]
        public float    TrapChance              = 15f;
        public bool     RandomStops             = false;
        public bool     StopAtWaypoint          = true;
        [Range(0f, 10f)]
        public float    MinStopDuration         = 1f;
        [Range(0f, 10f)]
        public float    MaxStopDuration         = 3f;
        [Range(0f, 10f)]
        public float    MovementSpeed           = 4f;
        public bool     RotateOnStop            = true;
        [Range(0f, 90f)]
        public float    RotationXmin            = 85;
        [Range(0f, 90f)]
        public float    RotationXmax            = 90;
        [Range(5f, 10f)]
        public float    ShootingForceMin        = 8;
        [Range(5f, 10f)]
        public float    ShootingForceMax        = 8;
        public bool     ShootTimerActive        = false;
        public bool     ShootAtWaypoint         = true;
        [Range(1f, 10f)]
        public float    ShootingTimerInterval   = 1;
        public int      Burstrate               = 2;
        [Range(0f, 1f)]
        public float    BurstInverval           = 0.4f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
