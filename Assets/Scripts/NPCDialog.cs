using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NPCDialog : MonoBehaviour
{
    // Start is called before the first frame update

    private List<DialogInfo[]> dialogInfoList;
    private int contentIndex;
    public Animator animator;
    void Start()
    {
        dialogInfoList = new List<DialogInfo[]>()
        {
            //0
            new DialogInfo[]
            {
                new DialogInfo() {name="Luna",content="hello,我是Luna,你可以用上下左右控制我移动，空格键与NPC对话，战斗中简单点击执行相应行为"}
            },

            //1
            new DialogInfo[]
            {
                new DialogInfo(){ name="Nala",content="好久不见了，小猫咪"},
                new DialogInfo(){name="Luna",content="Nala,好久不见呀，你还是这么有活力呢，喵~"},
                new DialogInfo(){name="Nala",content="还好吧~"},
                new DialogInfo(){name="Luna",content="我的狗一直在叫，但我这会忙不过来，你能帮我安抚一下吗？"},
                new DialogInfo(){name="Nala",content="啊？"},
                new DialogInfo(){name="Luna",content="摸摸它就行"},
                new DialogInfo(){name="Nala",content="可是。。。"},
                new DialogInfo(){name="Luna",content="我是猫女郎"},
                new DialogInfo(){name="Nala",content="安心啦，不会咬你的，去吧去吧~"},
            },

            //2
            new DialogInfo[]
            {
                new DialogInfo() {name="Nala",content="他还在叫呢"}
            },

            //3
            new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="感谢你Luna,你还是怎么可靠！"},
                new DialogInfo(){name="Nala",content="我想请你帮个忙好吗"},
                new DialogInfo(){name="Nala",content="说起来这事怪我。。"},
                new DialogInfo(){name="Nala",content="今天我睡过头了，出门比较匆忙"},
                new DialogInfo(){name="Nala",content="然后装蜡烛的口袋没有封好"},
                new DialogInfo(){name="Nala",content="结果就是蜡烛全部掉完了"},
                new DialogInfo(){name="Luna",content="你还是老样子，哈哈"},
                new DialogInfo(){name="Nala",content="所以，帮帮忙，好吗？"},
                new DialogInfo(){name="Nala",content="如果你能帮我找回来，我就送你一把神器"},
                new DialogInfo(){name="Luna",content="神器？"},
                new DialogInfo(){name="Nala",content="是的，我感觉很适合你，加油纳"},
            },

            //4
            new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="你还没收集到所有的蜡烛，宝"}
            },

            //5
            new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="可靠啊！这是你的奖赏"},
                new DialogInfo(){name="Luna",content="哇，这就是传说中的神器"},
                new DialogInfo(){name="Nala",content="请带着这把神器去清理附件的怪物吧"}
            },

            //6
            new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="宝，你还没清理干净呢"}
            },

            //7
            new DialogInfo[]
            {
                 new DialogInfo(){name="Nala",content="哇，这么厉害"},
                 new DialogInfo(){name="Luna",content="这把神器真厉害"}
            },

            //8
            new DialogInfo[]
            {
                new DialogInfo(){name="Nala",content="改天见"}
            }   
        };

        gameManager.instance.dialogInfoIndex = 0;
        contentIndex = 0;
        
    }


    public void DisPlayDialog()
    {
        if(gameManager.instance.dialogInfoIndex>7)
        {
            return;
        }

        animator.SetTrigger("Talk");

        if (contentIndex >= dialogInfoList[gameManager.instance.dialogInfoIndex].Length)
        {
            if(gameManager.instance.dialogInfoIndex==2 && !gameManager.instance.hasPetTheDog)
            {

            }
            else if(gameManager.instance.dialogInfoIndex==4 && gameManager.instance.candleNum < 3)
            {

            }
            else if(gameManager.instance.dialogInfoIndex== 6 && gameManager.instance.killNum < 3)
            {

            }
            else
            {
                gameManager.instance.dialogInfoIndex++;
            }
            if(gameManager.instance.dialogInfoIndex==6)
            {
                gameManager.instance.ShowMonsters();
            }
            
            contentIndex = 0;
            UIManager.Instance.ShowDialog();
            gameManager.instance.canControlLuna = true;
        }
        else
        {
            DialogInfo dialogInfo = dialogInfoList[gameManager.instance.dialogInfoIndex][contentIndex];
            UIManager.Instance.ShowDialog(dialogInfo.content, dialogInfo.name);
            contentIndex++;
            animator.SetTrigger("Talk");
        }

    }
    public void SetContentIndex()
    {
        contentIndex = dialogInfoList[gameManager.instance.dialogInfoIndex].Length;
    }
}

//对话信息类外结构体
public struct DialogInfo
{
    public string name;
    public string content;
}

