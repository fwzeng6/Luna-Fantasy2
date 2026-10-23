using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpArea : MonoBehaviour
{
    public Transform jumpPointA;
    public Transform jumpPointB;

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if(collision.CompareTag("Luna"))
        {
            LunaController lunaController = collision.GetComponent<LunaController>();//transform是collision上的一个组件，通过他可以获得collision上的其他组件
            lunaController.Jump(true);
            float distanceA= Vector3.Distance(lunaController.transform.position, jumpPointA.transform.position);
            float distanceB = Vector3.Distance(lunaController.transform.position, jumpPointB.transform.position);
            Transform targetTrans;
            if(distanceA>distanceB)//离B近，跳A
            {
                targetTrans = jumpPointA;
            }
            else   //跳B
            {
                targetTrans = jumpPointB;
            }
            lunaController.transform.DOMove(targetTrans.position, 0.5f).SetEase(Ease.Linear).OnComplete(() => { EndJump(lunaController); });
            Transform lunaLocalTrans = lunaController.transform.GetChild(0);
            Sequence sequence = DOTween.Sequence();//序列,里面可以存很多动画
            sequence.Append(lunaLocalTrans.DOLocalMoveY(1.2f, 0.25f).SetEase(Ease.InOutSine));
            sequence.Append(lunaLocalTrans.DOLocalMoveY(0.59f, 0.25f).SetEase(Ease.InOutSine));
            sequence.Play();

           

           


        }
    }

    private void EndJump(LunaController lunaController)
    {
        lunaController.Jump(false);
    }
}
