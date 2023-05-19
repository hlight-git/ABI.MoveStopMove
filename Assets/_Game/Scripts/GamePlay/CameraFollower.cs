using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    public enum State { MainMenu, Gameplay, Shop }
    Transform TF;
    public bool isTest;
    public Vector3 testOffset;

    [Header("Rotation")]
    [SerializeField] Vector3 playerRotate;
    [SerializeField] Vector3 gamePlayRotate;

    [Header("Offset")]
    [SerializeField] Vector3 playerOffset;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 offsetMax;
    [SerializeField] Vector3 offsetMin;

    [SerializeField] Transform[] offsets;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform target;

    private Vector3 targetOffset;
    private Quaternion targetRotate;

    public Camera Camera { get; private set; }

    private void Awake()
    {
        TF = transform;
        //target = FindObjectOfType<Player>().transform;
        Camera = Camera.main;
    }

    private void LateUpdate()
    {
        if (isTest)
        {
            TF.position = target.position + testOffset;
            return;
        }
        offset = Vector3.Lerp(offset, targetOffset, Time.deltaTime * moveSpeed);
        TF.rotation = Quaternion.Lerp(TF.rotation, targetRotate, Time.deltaTime * moveSpeed);
        //TF.position = Vector3.Lerp(TF.position, target.position + targetOffset, Time.deltaTime * moveSpeed);
        TF.position = target.position + targetOffset;
    }

    //rate
    public void SetRateOffset(float rate)
    {
        targetOffset = Vector3.Lerp(offsetMin, offsetMax, rate);
    }

    public void ChangeState(State state)
    {
        targetOffset = offsets[(int)state].localPosition;
        targetRotate = offsets[(int)state].localRotation;
        return;

        switch (state)
        {
            case State.MainMenu:
                targetOffset = playerOffset;
                targetRotate = Quaternion.Euler(playerRotate);
                break;

            case State.Gameplay:
                targetOffset = offsetMin;
                targetRotate = Quaternion.Euler(gamePlayRotate);
                break;

            default:
                break;
        }
    }
}