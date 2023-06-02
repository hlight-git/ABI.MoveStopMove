using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;

    private readonly List<Bot> bots = new List<Bot>();
    private List<UnitColor> notActiveColors;
    private int botRemain;
    private bool isRevived;

    public Player Player;
    public int Alives => botRemain + bots.Count + 1;
    public Level CurrentLevel { get; private set; }
    public List<ICharacter> PlayingCharacter { get; private set; }

    public void Start()
    {
        OnInit();
    }
    public void OnStartNormalMode()
    {
        Player.SetName(UserData.Ins.Name);
        SetTargetIndicatorsAlpha(1);
    }
    public void OnInitBots()
    {
        for (int i = 0; i < CurrentLevel.MaxActiveBotLimit; i++)
        {
            NewBot();
        }
        botRemain = CurrentLevel.TotalCharacterAmount - bots.Count - 1;
    }
    void NewBot()
    {
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, RandomSpawnPoint(), Quaternion.identity);
        bot.OnSpawn();
        bot.AddScore(Mathf.Clamp(Player.Score > 0 ? Random.Range(Player.Score - 2, Player.Score + 5) : Random.Range(0, 2), 0, CurrentLevel.TotalCharacterAmount / 2));
        bot.DodgingLevel = Alives < CurrentLevel.TotalCharacterAmount / 3 ? 1 : 2f * bot.Score / CurrentLevel.TotalCharacterAmount;
        bot.ChangeColor(Util.Choice(notActiveColors));
        bot.OnDeathEvents -= OnBotDeath;
        bot.OnDeathEvents += OnBotDeath;
        notActiveColors.Remove(bot.Color);
        if (GameManager.Ins.IsPlaying && Util.RandomBool(2/3f))
        {
            bot.RoamOrChase();
        }
        bots.Add(bot);
        PlayingCharacter.Add(bot);
    }
    void OnInitPlayer()
    {
        Player.OnSpawn();
        Player.AddScore(0);
        Player.OnDeathEvents -= OnPlayerDeath;
        Player.OnDeathEvents += OnPlayerDeath;
        notActiveColors.Remove(Player.Color);
    }
    public void OnInit()
    {
        LoadCurrentLevel();
        isRevived = false;
        notActiveColors = new List<UnitColor>(Constant.Prototype.UNIT_COLORS);
        PlayingCharacter = new List<ICharacter> { Player };
        OnInitPlayer();
        OnInitBots();
    }
    public void OnPlayerDeath(ICharacter player)
    {
        SoundManager.Ins.PlayAtPosition(SoundManager.Ins.RangerDeath, player.TF.position);
        UIManager.Ins.CloseAll();

        if (!isRevived)
        {
            isRevived = true;
            UIManager.Ins.OpenUI<UIRevive>();
        }
        else
        {
            Fail();
        }
    }
    void CreateBoosterBox()
    {
        if (Util.RandomBool(0.1f))
        {
            Vector3 spawnPoint = RandomPoint();
            spawnPoint.y = 0;
            SimplePool.Spawn<RangerBoosterBox>(PoolType.RangerBoosterBox, spawnPoint, Quaternion.identity).OnSpawn();
        }
    }
    public void OnBotDeath(ICharacter bot)
    {
        CreateBoosterBox();
        SoundManager.Ins.PlayAtPosition(SoundManager.Ins.RangerDeath, bot.TF.position);

        PlayingCharacter.Remove(bot);
        bots.Remove((Bot)bot);
        notActiveColors.Add(bot.Color);
        if (botRemain > 0)
        {
            botRemain--;
            NewBot();
            //Invoke(nameof(NewBot), GameConstant.Character.BOT_REVIVE_TIME);
        }
        if (botRemain == 0 && bots.Count == 0)
        {
            Victory();
        }
        UIManager.Ins.GetUI<UIGameplay>().UpdateTotalCharacter();
    }
    public void OnReset()
    {
        Player.OnDespawn();
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].OnDespawn();
        }
        bots.Clear();
        SimplePool.CollectAll();
    }
    public void ReturnMainMenu()
    {
        CancelInvoke();
        UIManager.Ins.CloseAll();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        OnReset();
        OnInit();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }
    public void OnLoadLevel(int level)
    {
        if (CurrentLevel != null)
        {
            Destroy(CurrentLevel.gameObject);
        }

        CurrentLevel = Instantiate(levels[level]);
        botRemain = CurrentLevel.TotalCharacterAmount - 1;
    }
    public Vector3 RandomPoint() => CurrentLevel.RandomPosition();
    Vector3? TrySpawnAtSpawnPoint(float size)
    {
        for (int i = 0; i < CurrentLevel.SpawnPoints.Count; i++)
        {
            bool meetOther = false;
            for (int j = 0; j < PlayingCharacter.Count; j++)
            {
                if (Vector3.Distance(CurrentLevel.SpawnPoints[i].position, PlayingCharacter[j].TF.position) < size)
                {
                    meetOther = true;
                    break;
                }
            }
            if (!meetOther)
            {
                return CurrentLevel.NavMeshSamplePosition(CurrentLevel.SpawnPoints[i].position);
            }
        }
        return null;
    }
    Vector3? TrySpawnAtRandomPoint(float size)
    {
        for (int tryTime = 0; tryTime < 50; tryTime++)
        {
            Vector3 result = RandomPoint();
            bool meetOther = false;
            for (int j = 0; j < PlayingCharacter.Count; j++)
            {
                if (Vector3.Distance(result, PlayingCharacter[j].TF.position) < size)
                {
                    meetOther = true;
                    break;
                }
            }
            if (!meetOther)
            {
                return result;
            }
        }
        return null;
    }
    public Vector3 RandomSpawnPoint()
    {
        float size = Constant.Ranger.DEFAULT_ATTACK_RANGE + Constant.Ranger.MAX_SIZE + 1f;
        Vector3? spawnPoint = (TrySpawnAtSpawnPoint(size) ?? TrySpawnAtRandomPoint(size)) ?? RandomPoint();
        return spawnPoint.Value;
    }

    private void Victory()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIVictory>().SetCoin(Player.Coin);
        Player.Cheering();
    }

    public void Fail()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIFail>().SetCoin(Player.Coin);
    }

    public void NextLevel()
    {
        UserData.Ins.SetIntData(UserData.KEY_LEVEL, ref UserData.Ins.Level, UserData.Ins.Level + 1);
    }
    public void LoadCurrentLevel()
    {
        OnLoadLevel(UserData.Ins.Level);
    }

    public void OnRevive()
    {
        Player.TF.position = RandomPoint();
        Player.OnRevive();
    }

    public void SetTargetIndicatorsAlpha(float alpha)
    {
        for (int i = 0; i < PlayingCharacter.Count; i++)
        {
            PlayingCharacter[i].SetIndicatorAlpha(alpha);
        }
    }
}
