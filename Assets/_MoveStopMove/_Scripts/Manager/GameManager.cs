using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Finish, Revive, Setting }

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentState {  get; private set; }
    private GameState gameState;

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
        CurrentState = gameState;
    }

    public bool IsState(GameState gameState) => this.gameState == gameState;
    public bool IsPlaying => !IsState(GameState.MainMenu);

    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        LoadUserData();
    }

    private void Start()
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
    }
    void LoadUserData()
    {
        UserData.Ins.LoadData();
        InjectUserData();

        if (UserData.Ins.IsFirstLoad)
        {
            UserData.Ins.SetEnumData(UserData.DEFAULT_WEAPON.ToString(), ShopItem.State.Equipped);
            UserData.Ins.SetEnumData(UserData.DEFAULT_HAT.ToString(), ShopItem.State.Equipped);
            UserData.Ins.SetEnumData(UserData.DEFAULT_PANT.ToString(), ShopItem.State.Equipped);
            UserData.Ins.SetEnumData(UserData.DEFAULT_ACCESSORY.ToString(), ShopItem.State.Equipped);
            UserData.Ins.SetEnumData(UserData.DEFAULT_SKIN.ToString(), ShopItem.State.Equipped);
            UserData.Ins.SetEnumData(UserData.DEFAULT_COLOR.ToString(), ShopItem.State.Equipped);
        }
        UserData.Ins.SetBoolData(UserData.KEY_FIRSTLOAD, ref UserData.Ins.IsFirstLoad, false);
    }
    public void InjectUserData()
    {
        LevelManager.Ins.Player.CreateBody(
            UserData.Ins.Skin,
            UserData.Ins.Weapon,
            UserData.Ins.Hat,
            UserData.Ins.Accessory,
            UserData.Ins.Pant,
            UserData.Ins.Color
        );
    }
}
