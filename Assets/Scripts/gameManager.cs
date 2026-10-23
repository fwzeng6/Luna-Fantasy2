using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public int contentIndex;
    public static gameManager instance;

    public int lunaHP; //最大生命值
    //public int lunaHP { get { return lunaHP; } }
    public float lunaCurrentHP;//Luna的当前生命值
    //public int Health { get { return lunaCurrentHP; } } //只能访问，不能修改，如要修改要set{lunaCurrentHP=value;}

    public GameObject battleGo;//战斗场景游戏物体

    public int lunaMP;//最大蓝量
    public float lunaCurrentMP;//当前蓝量

    public int monsterHP;
    public int monsterCurrentHP;

    public int dialogInfoIndex;
    public bool canControlLuna;

    public bool hasPetTheDog;
    public int candleNum;
    public int killNum;
    public GameObject monstersGo;
    public NPCDialog npc;

    public bool enterBattle;
    public GameObject battleMonsterGo;

    public AudioSource audioSourse;
    public AudioClip normalClip;
    public AudioClip battleClip;  


    public void Awake()
    {
        instance = this;
        lunaHP = 100;
        lunaCurrentHP = 100;
        lunaMP = 100;
        lunaCurrentMP = 100;

        monsterHP = 80;
        monsterCurrentHP = 80;
    }


    private void Update()
    {

        //非战斗状态下随时间自动回血逻辑
        if (!enterBattle)
        {
            if (lunaCurrentMP <= 100)
            {
                AddOrDecreaseMP(Time.deltaTime);
            }
            if (lunaCurrentHP <= 100)
            {
                AddOrDecreaseHP(Time.deltaTime);
            }
        }
    }
    //public void ChangeHeath(int amount)
    //{
    //    lunaCurrentHP = Mathf.Clamp(lunaCurrentHP + amount, 0, lunaHP);
    //    Debug.Log(lunaCurrentHP + "/" + lunaHP);//输出
    //}



    public void EnterOrExitBattle(bool enter = true,int addKillNum=0) //设置enter默认值为true,调用时可不传参
    {
        UIManager.Instance.ShowOrHideBatterPanel(enter);
        battleGo.SetActive(enter);
        if(!enter) //结束战斗
        {
            killNum += addKillNum;
            if(addKillNum>0)
            {
                DestroyMonster();
            }
            monsterCurrentHP = 50;
            PlayMusic(normalClip);
            if(lunaCurrentHP<=0)
            {
                lunaCurrentHP = 100;
                lunaCurrentMP = 0;
                battleMonsterGo.transform.position += new Vector3(0, 2, 0);
            }
        }
        else
        {
            PlayMusic(battleClip);
        }
        enterBattle = enter;

    }

    public void AddOrDecreaseMonsterHP(int value)
    {
        monsterCurrentHP += value;

        if (monsterCurrentHP <= 0)
        {
            monsterCurrentHP = 0;
        }



    }

    //luna血量改变
    public void AddOrDecreaseHP(float value)
    {
        lunaCurrentHP += value;
        if (lunaCurrentHP >= lunaHP)
        {
            lunaCurrentHP = lunaHP;
        }

        if (lunaCurrentHP <= 0)
        {
            lunaCurrentHP = 0;
        }

        UIManager.Instance.setHPValue(lunaCurrentHP / lunaHP);


    }


    public void AddOrDecreaseMP(float value)
    {
        lunaCurrentMP += value;
        if (lunaCurrentMP >= lunaMP)
        {
            lunaCurrentMP = lunaMP;
        }

        if (lunaCurrentMP <= 0)
        {
            lunaCurrentMP = 0;
        }

        UIManager.Instance.setMPValue(lunaCurrentMP / lunaMP);
    }

    /// <summary>
    /// 是否可以使用相关技能
    /// </summary>
    /// <param name="value">技能耗蓝量</param>
    /// <returns></returns>
    public bool CanUsePlayerMP(int value)
    {
        return lunaCurrentMP >= value;
    }

    public void ShowMonsters()
    {
        if (!monstersGo.activeSelf)
        {
            monstersGo.SetActive(true);
        }
    }

    public void DestroyMonster()
    {
        Destroy(battleMonsterGo);
    }

    public void SetMonster(GameObject go)
    {
        battleMonsterGo = go;

    }



    //任务完成设置索引
    public void SetContentIndex()
    { 
       npc.SetContentIndex();
    }

    public void PlayMusic(AudioClip audioClip)
    {
        if(audioSourse.clip!=audioClip)
        {
            audioSourse.clip = audioClip;
            audioSourse.Play();
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        if(audioClip)
        {
            audioSourse.PlayOneShot(audioClip);
        }
    }
   
}
