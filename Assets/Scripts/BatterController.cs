using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterController : MonoBehaviour
{

    public Animator LunaBatterAnimator;
    public Animator MonsterBatterAnimator;
    public Transform lunaTrans;
    public Transform monsterTrans;
    private Vector3 monsterInitPos;
    private Vector3 lunaInitPos;
    public SpriteRenderer monsterSr;
    public SpriteRenderer lunaSr;
    public GameObject skillEffectGo;
    public GameObject healEffectGo;
    public AudioClip attackSound;
    public AudioClip lunaAttackSound;
    public AudioClip monsterAttackSound;
    public AudioClip skillSound;
    public AudioClip recoverSound;
    public AudioClip dieSound;
    public AudioClip hitSound;
    public AudioClip monsterDieSound;
    

    private void Awake()//只调用一次
    {
        monsterInitPos = monsterTrans.localPosition;
        lunaInitPos = lunaTrans.localPosition;

    }

    private void OnEnable()  //在panel打开时就调用
    {
        lunaSr.DOFade(1, 0);
        monsterSr.DOFade(1, 0);
        lunaTrans.localPosition = lunaInitPos;
        monsterTrans.localPosition = monsterInitPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LunaAttack() 
    {
        StartCoroutine(PerformAttackLogic());
        
    }

    IEnumerator PerformAttackLogic()
    {
        UIManager.Instance.ShowOrHideBatterPanel(false);
        LunaBatterAnimator.SetBool("MoveState",true);
        LunaBatterAnimator.SetFloat("MoveValue", -1);
        lunaTrans.DOLocalMove(monsterInitPos + new Vector3(1, 0, 0), 0.5f).OnComplete
            (
              () =>
              {
              gameManager.instance.PlaySound(attackSound);
              gameManager.instance.PlaySound(lunaAttackSound);
              LunaBatterAnimator.SetBool("MoveState", false);
              LunaBatterAnimator.SetFloat("MoveValue", 0);
              LunaBatterAnimator.CrossFade("Attack", 0);
              monsterSr.DOFade(0.2f, 0.2f).OnComplete(() => { JudgeMonsterHP(-20); });
              }
            );//DOLocalMove和DOFade是DOtween插件方法

        yield return new WaitForSeconds(1.167f);

        LunaBatterAnimator.SetBool("MoveState", true);
        LunaBatterAnimator.SetFloat("MoveValue", 1);
        lunaTrans.DOLocalMove(lunaInitPos, 0.5f).OnComplete(() =>
        {
            LunaBatterAnimator.SetBool("MoveState", false);
        });

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(MonsterAttack());
    }

    IEnumerator MonsterAttack()
    {
        monsterTrans.DOLocalMove(lunaInitPos + new Vector3(-1.5f, 0, 0), 0.5f);

        yield return new WaitForSeconds(0.5f);

        monsterTrans.DOLocalMove(lunaInitPos + new Vector3(-0.5f,0,0),0.2f).OnComplete(() =>
        {
            gameManager.instance.PlaySound(monsterAttackSound);
            monsterTrans.DOLocalMove(lunaInitPos + new Vector3(-1.5f, 0, 0), 0.2f);
            LunaBatterAnimator.CrossFade("Hit", 0);
            gameManager.instance.PlaySound(hitSound);
            lunaSr.DOFade(0.3f, 0.3f).OnComplete(() =>
            {
                JudgeLunaHP(-20);
            });
        
        }
        );

        yield return new WaitForSeconds(0.4f);

        monsterTrans.DOLocalMove(monsterInitPos, 0.5f).OnComplete(() =>
        {
            UIManager.Instance.ShowOrHideBatterPanel(true);
        });

        
    }


    public void LunaDefend()
    {
        StartCoroutine(PerformDefendLogic());
    }



    IEnumerator PerformDefendLogic()
    {
        UIManager.Instance.ShowOrHideBatterPanel(false);

        LunaBatterAnimator.SetBool("Defend", true);


        monsterTrans.DOLocalMove(lunaInitPos + new Vector3(-1.5f, 0, 0), 0.5f);

        yield return new WaitForSeconds(0.5f);

        monsterTrans.DOLocalMove(lunaInitPos + new Vector3(-0.5f, 0, 0), 0.2f).OnComplete(() =>
        {
            gameManager.instance.PlaySound(monsterAttackSound);
            monsterTrans.DOLocalMove(lunaInitPos + new Vector3(-1.5f, 0, 0), 0.2f);
            lunaTrans.DOLocalMove(lunaInitPos + new Vector3(1.0f, 0, 0), 0.2f).OnComplete(() =>
            {
                lunaTrans.DOLocalMove(lunaInitPos, 0.2f);
            });
        }
        );

        yield return new WaitForSeconds(0.4f);

        monsterTrans.DOLocalMove(monsterInitPos, 0.5f).OnComplete(() =>
        {
            UIManager.Instance.ShowOrHideBatterPanel(true);
            LunaBatterAnimator.SetBool("Defend", false);
        });

    }

    public void LunaUseSkill()
    {
        if(!gameManager.instance.CanUsePlayerMP(30))
        {
            return;
        }
        StartCoroutine(PerformSkillLogic());
    }

    IEnumerator PerformSkillLogic()
    {
        UIManager.Instance.ShowOrHideBatterPanel(false);
        LunaBatterAnimator.CrossFade("Skill", 0);
        gameManager.instance.AddOrDecreaseMP(-30);
        yield return new WaitForSeconds(0.5f);
        GameObject go=Instantiate(skillEffectGo, monsterTrans);
        go.transform.localPosition = Vector3.zero;
        gameManager.instance.PlaySound(lunaAttackSound);
        gameManager.instance.PlaySound(skillSound);
        yield return new WaitForSeconds(0.4f);
        monsterSr.DOFade(0.3f, 0.2f).OnComplete(() =>
        {
            JudgeMonsterHP(-30);
        });
        yield return new WaitForSeconds(0.4f);
        if(gameManager.instance.monsterCurrentHP>0)
        {
            StartCoroutine(MonsterAttack());
        }
        

        UIManager.Instance.ShowOrHideBatterPanel(true);
    }

    public void LunaRecoverHP()
    {
        if (!gameManager.instance.CanUsePlayerMP(50))
        {
            return;
        }
        StartCoroutine(PerformRecoverHPLogic());
    }

    IEnumerator PerformRecoverHPLogic()
    {
        UIManager.Instance.ShowOrHideBatterPanel(false);
        LunaBatterAnimator.CrossFade("RecoverHP", 0);
        gameManager.instance.AddOrDecreaseHP(30);
        gameManager.instance.AddOrDecreaseMP(-50);
        gameManager.instance.PlaySound(recoverSound);
        gameManager.instance.PlaySound(lunaAttackSound);
        yield return new WaitForSeconds(0.5f);
        GameObject go = Instantiate(healEffectGo,lunaTrans);
        go.transform.localPosition = Vector3.zero;
        gameManager.instance.AddOrDecreaseHP(40);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MonsterAttack());

    }

    private void JudgeLunaHP(int value)
    {
        
        gameManager.instance.AddOrDecreaseHP(value);
        if(gameManager.instance.lunaCurrentHP<=0)
        {
            LunaBatterAnimator.CrossFade("Die", 0);
            gameManager.instance.PlaySound(dieSound);
            lunaSr.DOFade(0, 0.8f).OnComplete(() =>
            {
                gameManager.instance.EnterOrExitBattle(false);
                UIManager.Instance.ShowOrHideBatterPanel(false);
            });
        }
        else if(gameManager.instance.lunaCurrentHP>0)
        {
            lunaSr.DOFade(1, 0.2f);
        }
    }
    
    private void JudgeMonsterHP(int value)
    {
        gameManager.instance.AddOrDecreaseMonsterHP(value);
        if (gameManager.instance.monsterCurrentHP <= 0)
        {
            gameManager.instance.PlaySound(monsterDieSound);
            monsterSr.DOFade(0, 0.8f).OnComplete(() =>
            {
                gameManager.instance.EnterOrExitBattle(false,1);
                UIManager.Instance.ShowOrHideBatterPanel(false);
            });
        }
        else if (gameManager.instance.monsterCurrentHP > 0)
        {
            monsterSr.DOFade(1, 0.2f);
        }
    }

    public void LunaEscape()
    {
        UIManager.Instance.ShowOrHideBatterPanel(false);
        
        LunaBatterAnimator.SetFloat("MoveValue", 1);
        LunaBatterAnimator.SetBool("MoveState", true);
        lunaTrans.DOLocalMove(lunaInitPos + new Vector3(5,0,0), 0.7f).OnComplete(() =>
        {
            gameManager.instance.EnterOrExitBattle(false);
            
            
        });

       
    }
}
