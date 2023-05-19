using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        joystickRender.SetActive(true);
        renderRectTransform.position = eventData.position;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        joystickRender.SetActive(false);
    }
    protected override void OnInit()
    {
        base.OnInit();
        //LevelManager.Instance.OnGameStartEvents += OnGameStart;
        //LevelManager.Instance.OnGameOverEvents += OnGameOver;
    }
    //void OnGameStart()
    //{
    //    gameObject.SetActive(true);
    //}
    //void OnGameOver()
    //{
    //    gameObject.SetActive(false);
    //}
    //void OnGameStart(Player player)
    //{
    //    OnGameStart();
    //    SetTarget(player.Movement);
    //}
    //void OnGameOver(Character character)
    //{
    //    OnGameOver();
    //}
}
