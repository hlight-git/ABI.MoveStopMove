using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    public enum State { MainMenu, Gameplay, Shop }
    Transform TF;

    [Header("Offset following player size:")]
    [SerializeField] Transform offsetMaxTF;
    [SerializeField] Transform offsetMinTF;

    [SerializeField] Transform[] statePositions;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform target;

    private Vector3 targetOffset;
    private Quaternion targetRotate;
    public bool isTest;
    public Vector3 testOffset;

    public Camera Camera { get; private set; }

    private void Awake()
    {
        TF = transform;
        Camera = Camera.main;
    }

    private void LateUpdate()
    {
        if (isTest)
        {
            Vector3 desiredPosition = target.position + testOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * moveSpeed);
            TF.position = smoothedPosition;
            return;
        }
        TF.SetPositionAndRotation(
            Vector3.Lerp(TF.position, target.position + targetOffset, Time.deltaTime * moveSpeed),
            Quaternion.Lerp(TF.rotation, targetRotate, Time.deltaTime * moveSpeed)
        );
    }

    //rate
    //public void SetRateOffset(float rate)
    //{
    //    targetOffset = Vector3.Lerp(offsetMinTF.localPosition, offsetMaxTF.localPosition, rate);
    //}
    public void SetDistanceToTarget(float distanceCoeff)
    {
        targetOffset = - distanceCoeff * TF.forward;
    }
    public void ChangeState(State state)
    {
        targetOffset = statePositions[(int)state].localPosition;
        targetRotate = statePositions[(int)state].localRotation;
        return;
    }
}
