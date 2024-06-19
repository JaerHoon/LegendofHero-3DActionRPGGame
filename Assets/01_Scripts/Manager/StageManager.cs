using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StageManager : MonoBehaviour
{
    public GameObject Knight;
    public GameObject Archer;

    CinemachineVirtualCamera fallowCam;

    public List<StageData> stageDatas = new List<StageData>();
    [SerializeField]
    GameObject spawnPosParent;
    List<Vector3> SpawnPos = new List<Vector3>();

    List<GameObject> Monsters = new List<GameObject>();

    InGameCanvasController inGameCanvasController;
    GameObject NPCPlatform;
    NPCMage npcMage;
    NextStagePortal nextStagePortal;

    [SerializeField]
    GameObject ChoicePlatforms;

    GameObject skill_NormalATK;
    SkillChoiceController normalController;

    GameObject skill_SkillATK;
    SkillChoiceController skillController;

    GameObject itemRelicATK;
    SkillChoiceController itemRelicController;
    [HideInInspector]
    public int currentStageNum;

   public  Character Player;
    Vector3 StartPos = new Vector3(0, 0, -7);

    private void Start()
    {
        inGameCanvasController = GameObject.FindFirstObjectByType<InGameCanvasController>();
        NPCPlatform = GameObject.FindGameObjectWithTag("NPCPlatform");
        npcMage = GameObject.FindFirstObjectByType<NPCMage>();
        nextStagePortal = GameObject.FindFirstObjectByType<NextStagePortal>();
        skill_NormalATK = ChoicePlatforms.transform.GetChild(0).gameObject;
        skill_SkillATK = ChoicePlatforms.transform.GetChild(1).gameObject;
        itemRelicATK = ChoicePlatforms.transform.GetChild(2).gameObject;
        normalController = skill_NormalATK.GetComponent<SkillChoiceController>();
        skillController = skill_SkillATK.GetComponent<SkillChoiceController>();
        itemRelicController = itemRelicATK.GetComponent<SkillChoiceController>();
        for(int i = 0; i < spawnPosParent.transform.childCount; i++)
        {
            Vector3 pos = spawnPosParent.transform.GetChild(i).transform.position;
            SpawnPos.Add(pos);
        }

        Invoke("GameStart", 1);
    }

    public void CreateCharacter()
    {
        fallowCam = FindFirstObjectByType<CinemachineVirtualCamera>();
        if (CharacterManager.instance.choicedCharacter == CharacterManager.ChoicedCharacter.Warrior)
        {
            GameObject play = Instantiate(Knight, Vector3.zero, Quaternion.identity);
            fallowCam.m_Follow = play.transform;
            fallowCam.m_LookAt = play.transform;
            Player = play.GetComponent<Character>();
            Player.OffPlay();
        }
        else
        {
            GameObject play = Instantiate(Archer, Vector3.zero, Quaternion.identity);
            fallowCam.m_Follow = play.transform;
            fallowCam.m_LookAt = play.transform;
            Player = play.GetComponent<Character>();
            Player.OffPlay();
        }
    }

    void GameStart()
    {
        EnterStage(0);
    }

    public void EnterStage(int stageNum)
    {
        Player.OffPlay();
        inGameCanvasController.OnFadeIn_Out(stageNum);
        currentStageNum = stageNum;
       
        Invoke("SetStage", 0.5f);
    }

    public void SetStage()
    {
        Player.gameObject.SetActive(true);
        NPCPlatform.SetActive(false);
        npcMage.gameObject.SetActive(false);
        nextStagePortal.gameObject.SetActive(false);
        itemRelicATK.SetActive(false);
        skill_NormalATK.SetActive(false);
        skill_SkillATK.SetActive(false);
        nextStagePortal.gameObject.SetActive(false);
        inGameCanvasController.SetPlayerInfo();
        if (stageDatas[currentStageNum].stageType == StageData.StageType.Start)
        {
            NPCPlatform.SetActive(true);
            npcMage.gameObject.SetActive(true);
            npcMage.stageType = NPCMage.StageType.Start;
            nextStagePortal.gameObject.SetActive(true);
            Player.transform.position = Vector3.zero;
            Player.OnPlay();
            

        }
        else if (stageDatas[currentStageNum].stageType == StageData.StageType.Market)
        {
            NPCPlatform.SetActive(true);
            npcMage.gameObject.SetActive(true);
            npcMage.stageType = NPCMage.StageType.Market;
            itemRelicATK.SetActive(true);
            itemRelicController.stageType = SkillChoiceController.StageType.Maerket;
            itemRelicController.Setting();
            nextStagePortal.gameObject.SetActive(true);
            Player.transform.position = StartPos;
            Player.OnPlay();
        }
        else
        {
            skillController.stageType = SkillChoiceController.StageType.stage;
            normalController.stageType = SkillChoiceController.StageType.stage;
            itemRelicController.stageType = SkillChoiceController.StageType.stage;
            SpawnMonsters(currentStageNum);
        }
    }

    void SpawnMonsters(int stageNum)
    {
        Monsters.Clear();


        for (int a = 0; a < stageDatas[stageNum].MinionCount; a++)
        {
            GameObject mon = PoolFactroy.instance.GetPool(Consts.MonsterMinion);
            Monsters.Add(mon);
        }
        for (int a = 0; a < stageDatas[stageNum].WarriorCount; a++)
        {
            GameObject mon = PoolFactroy.instance.GetPool(Consts.MonsterWarrior);
            Monsters.Add(mon);
        }

        for (int a = 0; a < stageDatas[stageNum].MageCount; a++)
        {
            GameObject mon = PoolFactroy.instance.GetPool(Consts.MonsterMage);
            Monsters.Add(mon);
        }

        for (int a = 0; a < stageDatas[stageNum].RogueCount; a++)
        {
            GameObject mon = PoolFactroy.instance.GetPool(Consts.MonsterRogue);
            Monsters.Add(mon);
        }
        for (int a = 0; a < stageDatas[stageNum].GolemCount; a++)
        {
            inGameCanvasController.OnGolemHP();
            GameObject mon = PoolFactroy.instance.GetPool(Consts.StoneGolem);
            Monsters.Add(mon);
            mon.transform.position = SpawnPos[0];
            
        }

        int[] num = RandomNumber.RandomCreate(stageDatas[stageNum].MonsterCount(), 0, SpawnPos.Count);

        for (int i = 0; i < num.Length; i++)
        {
            Monsters[i].transform.position = SpawnPos[num[i]];
        }

        PlayerReady();
    }

    void PlayerReady()
    {
        Player.transform.position = Vector3.zero;
        Player.OnPlay();
    }

    public void MonsterDie(GameObject Monster)
    {
        Monsters.Remove(Monster);
        if (Monsters.Count <= 0)
        {
            WinBattle();
        }
    }

    void WinBattle()
    {
        if(stageDatas[currentStageNum].stageType == StageData.StageType.NormalATK)
        {
           
            skill_NormalATK.SetActive(true);
            normalController.Setting();
        }
        else if(stageDatas[currentStageNum].stageType == StageData.StageType.SkillATK)
        {
          
            skill_SkillATK.SetActive(true);
            skillController.Setting();
        }
        else if(stageDatas[currentStageNum].stageType == StageData.StageType.ItemRelic)
        {
            
            itemRelicATK.SetActive(true);
            itemRelicController.Setting();
        }

        nextStagePortal.gameObject.SetActive(true);
    }

    public void PlayerDie()
    { 
        Player.PlayerReset();
        
        EnterStage(0);
    }

}
