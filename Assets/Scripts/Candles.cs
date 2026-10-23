using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Candles : MonoBehaviour
{
    public GameObject effectGo;
    public AudioClip pickClip;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.instance.candleNum++;
        Instantiate(effectGo, transform.position, Quaternion.identity);
        if(gameManager.instance.candleNum>=3)
        {
            gameManager.instance.SetContentIndex();
        }
        gameManager.instance.PlaySound(pickClip);
        Destroy(gameObject);
    }
}
