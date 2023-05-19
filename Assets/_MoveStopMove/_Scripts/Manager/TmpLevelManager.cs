using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TmpLevelManager : Singleton<TmpLevelManager>
{
    public TmpPlayer Player;
    public List<AbsCharacter> PlayingCharacter;

    private List<TmpBot> bots = new List<TmpBot>();

    [SerializeField] Level[] levels;
    public Level currentLevel;

    private int totalBot;
    private bool isRevive;

    private int levelIndex;

    public int TotalCharater => totalBot + bots.Count + 1;

    public void Start()
    {
        levelIndex = 0;
        //OnLoadLevel(levelIndex);
        OnInit();
    }
    public void InitCharacters()
    {
        PlayingCharacter = new List<AbsCharacter> { Player };

        // Player
        Player.OnInit();
        Player.GameStateUpdateOnDeathEvents += OnACharacterDeath;

        // Bots
        for (int i = 0; i < currentLevel.MaxActiveBotLimit; i++)
        {
            NewBot();
        }
    }
    public void OnInit()
    {
        InitCharacters();
        //for (int i = 0; i < currentLevel.botReal; i++)
        //{
        //    NewBot(null);
        //}


        //totalBot = currentLevel.botTotal - currentLevel.botReal - 1;

        //isRevive = false;

        //SetTargetIndicatorAlpha(0);
    }
    public void OnPlayerDeath(TmpPlayer player)
    {

    }
    public void OnACharacterDeath(AbsCharacter character)
    {
        PlayingCharacter.Remove(character);
        if (character !=  Player)
        {
            if (GameManager.Ins.CurrentState == GameState.GamePlay)
            {
                totalBot--;
            }
            Invoke(nameof(NewBot), GameConstant.Character.BOT_REVIVE_TIME);
        }
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

    public void OnLoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[level]);
    }
    public Vector3 RandomPoint() => currentLevel.RandomPosition();
    Vector3? TrySpawnAtSpawnPoint(float size)
    {
        for (int i = 0; i < currentLevel.SpawnPoints.Count; i++)
        {
            bool meetOther = false;
            for (int j = 0; j < PlayingCharacter.Count; j++)
            {
                if (Vector3.Distance(currentLevel.SpawnPoints[i].position, PlayingCharacter[j].TF.position) < size)
                {
                    meetOther = true;
                    break;
                }
            }
            if (!meetOther)
            {
                return currentLevel.NavMeshSamplePosition(currentLevel.SpawnPoints[i].position);
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
        float size = Character.ATT_RANGE + Character.MAX_SIZE + 1f;
        Vector3? spawnPoint = TrySpawnAtSpawnPoint(size);
        if (spawnPoint == null)
        {
            spawnPoint = TrySpawnAtRandomPoint(size);
        }
        if (spawnPoint == null)
        {
            spawnPoint = RandomPoint();
        }
        return spawnPoint.Value;
    }

    //private void NewBot(IState<Bot> state)
    //{
    //    Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, RandomPoint(), Quaternion.identity);
    //    bot.OnInit();
    //    bot.ChangeState(state);
    //    bots.Add(bot);

    //    bot.SetScore(player.Score > 0 ? Random.Range(player.Score - 7, player.Score + 7) : 1);
    //}
    private void NewBot()
    {
        TmpBot bot = SimplePool.Spawn<TmpBot>(PoolType.TmpBot, RandomSpawnPoint(), Quaternion.identity);
        bot.OnInit();
        //bot.ChangeState(state);
        bots.Add(bot);
        bot.GameStateUpdateOnDeathEvents += OnACharacterDeath;
        PlayingCharacter.Add(bot);

        //bot.SetScore(player.Score > 0 ? Random.Range(player.Score - 7, player.Score + 7) : 1);
    }

    //public void CharecterDeath(Character c)
    //{
    //    if (c is Player)
    //    {
    //        UIManager.Ins.CloseAll();

    //        //revive
    //        if (!isRevive)
    //        {
    //            isRevive = true;
    //            UIManager.Ins.OpenUI<UIRevive>();
    //        }
    //        else
    //        {
    //            Fail();
    //        }
    //    }
    //    else
    //    if (c is Bot)
    //    {
    //        bots.Remove(c as Bot);

    //        if (GameManager.Ins.IsState(GameState.Revive) || GameManager.Ins.IsState(GameState.Setting))
    //        {
    //            if (Utilities.Chance(50, 100))
    //            {
    //                NewBot(new IdleState());
    //            }
    //            else
    //            {
    //                NewBot(new PatrolState());
    //            }
    //        }
    //        else
    //        {
    //            if (totalBot > 0)
    //            {
    //                totalBot--;
    //                if (Utilities.Chance(50, 100))
    //                {
    //                    NewBot(new IdleState());
    //                }
    //                else
    //                {
    //                    NewBot(new PatrolState());
    //                }
    //            }

    //            if (bots.Count == 0)
    //            {
    //                Victory();
    //            }
    //        }

    //    }

    //    UIManager.Ins.GetUI<UIGameplay>().UpdateTotalCharacter();
    //}

    //private void Victory()
    //{
    //    UIManager.Ins.CloseAll();
    //    UIManager.Ins.OpenUI<UIVictory>().SetCoin(player.Coin);
    //    player.ChangeAnim(Constant.ANIM_WIN);
    //}

    //public void Fail()
    //{
    //    UIManager.Ins.CloseAll();
    //    UIManager.Ins.OpenUI<UIFail>().SetCoin(player.Coin);
    //}

    //public void Home()
    //{
    //    UIManager.Ins.CloseAll();
    //    OnReset();
    //    OnLoadLevel(levelIndex);
    //    OnInit();
    //    UIManager.Ins.OpenUI<UIMainMenu>();
    //}

    //public void NextLevel()
    //{
    //    levelIndex++;
    //}

    //public void OnPlay()
    //{
    //    for (int i = 0; i < bots.Count; i++)
    //    {
    //        bots[i].ChangeState(new PatrolState());
    //    }
    //}

    //public void OnRevive()
    //{
    //    player.TF.position = RandomPoint();
    //    player.OnRevive();
    //}

    //public void SetTargetIndicatorAlpha(float alpha)
    //{
    //    List<GameUnit> list = SimplePool.GetAllUnitIsActive(PoolType.TargetIndicator);

    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        (list[i] as TargetIndicator).SetAlpha(alpha);
    //    }
    //}
}
