using System.Collections;
using System.Collections.Generic;
using GlitchBallVR;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundConfig", menuName = "ScriptableObjects/RoundConfiguration", order = 1)]
public class RoundConfiguration : ScriptableObject
{
    public int Round = 0;
    public List<ShootingMode> ShootingPattern;
    public bool TrapsEnabled = false;
    [Range(0f, 100f)]
    public float TrapChance = 15f;
    public bool RandomStops = false;
    public bool StopAtWaypoint = true;
    [Range(0f, 10f)]
    public float MinStopDuration = 1f;
    [Range(0f, 10f)]
    public float MaxStopDuration = 3f;
    [Range(0f, 10f)]
    public float MovementSpeed = 4f;
    public bool RotateOnStop = true;
    [Range(0f, 90f)]
    public float RotationXmin = 85;
    [Range(0f, 90f)]
    public float RotationXmax = 90;
    [Range(0.1f, 50f)]
    public float ShootingForceMin = 8;
    [Range(0.1f, 50f)]
    public float ShootingForceMax = 8;
    public bool ShootTimerActive = false;
    public bool ShootAtWaypoint = true;
    [Range(1f, 10f)]
    public float ShootingTimerInterval = 1;
    public int Burstrate = 2;
    [Range(0f, 1f)]
    public float BurstInterval = 0.4f;
    public int ScoreToNextRound = 0;
}
