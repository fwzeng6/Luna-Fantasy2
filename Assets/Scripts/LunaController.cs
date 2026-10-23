using System.Collections;             //命名空间
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LunaController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float moveSpeed;
  
    private Animator animator;
    private Vector2 lookDirection = new Vector2(1, 0);
    private float moveScale;
    private Vector2 move;
    
    // Start is called before the first frame update
    void Start()          
    {
         Application.targetFrameRate=60;
        rigidbody2d = GetComponent<Rigidbody2D>();
       
        animator = GetComponentInChildren<Animator>();
        //animator.SetFloat("MoveValue",0.5f);
        //animator.SetBool("MoveState", true);
        //animator.SetTrigger("New Trigger");
    }

    // Update is called once per frame
    void Update()  //每秒调用大约60次
    {

        if (gameManager.instance.enterBattle)
        {
            return;
        }
        if (! gameManager.instance.canControlLuna)
        {
            return;
        }
        //玩家输入监听
        float horizontal= Input.GetAxisRaw("Horizontal");//获取玩家水平轴向输入
        float vertical = Input.GetAxisRaw("Vertical");  // 获取玩家垂直轴向输入，静态方法，所以用类名.方法名() ，不需要实例进行调用，全局可访问，不同脚本也可调用
        move = new Vector2(horizontal, vertical);

        //判断Luna是否有对应的看向方位
        if (!Mathf.Approximately(move.x,0)||!Mathf.Approximately(move.y,0))  //做对比，忽略误差
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
                //不需要大小
        }

        //动画的控制
        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
        moveScale = move.magnitude;
        if (move.magnitude > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveScale = 1;
                moveSpeed = 2;
            }
            else
            {
                moveScale = 2;
                moveSpeed = 5;
            } 
        }
        animator.SetFloat("MoveValue", moveScale);

        //检查是否与NPC对话
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Talk();
        }
       

    }

    private void FixedUpdate()
    {
        if (gameManager.instance.enterBattle)
        {
            return;
        }
        move.Normalize();
        Vector2 position = transform.position;
        position = position + moveSpeed * move * Time.fixedDeltaTime;//加了Time每秒3米,没加每秒180米
        rigidbody2d.MovePosition(position);//成员对象方法或普通方法，要用对象名.方法名（），不通过自己写的代码改变位置而是通过刚体的方法来改变位置
    }
   

    public void Climb(bool start)
    {
        animator.SetBool("Climb", start);
    }

    public void Jump(bool start)
    {
        animator.SetBool("Jump", start);
        rigidbody2d.simulated = !start;
    }

     public void Talk()
    {
        Collider2D collider = Physics2D.OverlapCircle(rigidbody2d.position, 0.5f, LayerMask.GetMask("NPC") );
        if(collider !=null)
        {
            if(collider.name=="Nala")
            {
                gameManager.instance.canControlLuna = false;
                collider.GetComponent<NPCDialog>().DisPlayDialog();
            }
            else if(collider.name=="Dog" && !gameManager.instance.hasPetTheDog && gameManager.instance.dialogInfoIndex==2)
            {
                PetTheDog();
                gameManager.instance.canControlLuna = false;
                collider.GetComponent<Dog>().BeHappy();
            }
        }
    }

    private void PetTheDog()
    {
        animator.CrossFade("PetTheDog", 0);
        transform.position = new Vector3(-1.19f, -7.8f, 0);
    }
} 
