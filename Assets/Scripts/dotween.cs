using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class dotween : MonoBehaviour
{
    public Transform targetTrans;
    private Tween tween;
    // Start is called before the first frame update
    void Start()
    {
        tween=transform.DOMove(targetTrans.position,2).OnComplete(DebugGameOver);//两秒移动到目标位置
        tween.Pause();
        tween.Kill();
        /*transform.DORotate(new Vector3(0,90,0),1);
        transform.DOShakePosition(1,1);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            tween.PlayBackwards();
          
           
        }
        else if(Input.GetMouseButtonDown(1))
        {
            tween.PlayForward();
            
        }

       
    }

    private void DebugGameOver()
    {
        Debug.Log("游戏结束了");
    }
}
