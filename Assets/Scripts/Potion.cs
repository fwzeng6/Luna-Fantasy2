using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject effectGo;
    public AudioClip pickSound;
    private void OnTriggerEnter2D(Collider2D collision) //参数是碰到谁就是谁
    {

        /* //Debug.Log("血瓶被"+collision+"吃到了！");
         LunaController lunaController= collision.GetComponent<LunaController>();
         if (lunaController != null)
         {

             if (lunaController.Health <lunaController.lunaHP)
             {
                 lunaController.ChangeHeath(1);
                 Instantiate(effectGo, transform.position, Quaternion.identity);
                 Destroy(gameObject); //把游戏物体potion销毁

             }

         }                        //判断是否为空，避免了lunaController为空时ChangeHeath(1)方法被调用从而引起报错;*/




        if (gameManager.instance.lunaCurrentHP < gameManager.instance.lunaHP)
        {
            gameManager.instance.AddOrDecreaseHP(40);
            Instantiate(effectGo, transform.position, Quaternion.identity);
            gameManager.instance.PlaySound(pickSound);
            Destroy(gameObject); //把游戏物体potion销毁

        }




        //private void OnTriggerStay2D(Collider2D collision)//自动调用
        //{
        //        Debug.Log("呆在里面");
        //    }

        //    private void OnTriggerExit2D(Collider2D collision)
        //    {
        //        Debug.Log("出来了");
        //    }
    }

}
