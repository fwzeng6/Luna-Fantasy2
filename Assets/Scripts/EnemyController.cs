using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public static EnemyController instance;
    //轴向控制
    public bool vertical;
    public float speed = 2;
    //刚体组件引用，为了使用刚体进行移动
    private Rigidbody2D rigidbody2d;
    //方向控制
    private int direction = 1;
    //方向改变时间间隔
    public float changeTime = 3.0f;
    //计时器
    private float timer;
    //动画控制器
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.instance.enterBattle)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;

        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = rigidbody2d.position;
        if(vertical)//垂直轴向移动
        {
            animator.SetFloat("LookX",0);
            animator.SetFloat("LookY", direction);
            pos.y = pos.y + speed *direction* Time.fixedDeltaTime;
        }
        else//水平轴向移动
        {
            animator.SetFloat("LookY", 0);
            animator.SetFloat("LookX", direction);
            pos.x = pos.x + speed * direction * Time.fixedDeltaTime;
        }

        rigidbody2d.MovePosition(pos);

        
        
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Luna"))
        {
            gameManager.instance.EnterOrExitBattle();
            gameManager.instance.SetMonster(gameObject);
        }
    }
}
