using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform GroundChecker;
    [SerializeField] float checkRadius = 0.2f; //GroundChecker
    [SerializeField] LayerMask layerMask; //GroundChecker
    private bool onGround;

    private CharacterController characterController;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] float speed = 3f;
    [SerializeField] float gravity = -19.8f;
    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float rotationSpeed = 4f;

    private Animator animator;
    private int attack_anictrl = 0;

    private bool Attacking = false;
    private bool Defending = false;
    private bool Die = false;
    private bool Dizzy = false;
    private bool Gethit = false;

    //Global
    private bool Global_Gethit = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = transform.GetComponent<CharacterController>();
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Attacking == true) 
        {
            CoolDown_Attack();
        }
        if(Dizzy == true) 
        {
            CoolDown_Dizzy();
        }
        if(Gethit == true)
        {
            CoolDown_Gethit();
        }


        MoveLikeTopDown();
        AnimatorControl();
    }

    private float Attack_timer = 0f;  // 計時器變數
    private float Attack_duration = 0.3f;  // 計時的總時間
    private void CoolDown_Attack()
    {
        // 更新計時器
        Attack_timer += Time.deltaTime;

        // 檢查是否超過了指定的時間
        if (Attack_timer >= Attack_duration)
        {
            Attacking = false;
            attack_anictrl = 0;

            // 重置計時器
            Attack_timer = 0f;
        }
    }

    private float Dizzy_timer = 0f;  // 計時器變數
    private float Dizzy_duration = 5f;  // 計時的總時間
    private void CoolDown_Dizzy()
    {
        // 更新計時器
        Dizzy_timer += Time.deltaTime;

        // 檢查是否超過了指定的時間
        if (Dizzy_timer >= Dizzy_duration)
        {
            Dizzy = false;

            // 重置計時器
            Dizzy_timer = 0f;
        }
    }

    private float Gethit_timer = 0f;  // 計時器變數
    private float Gethit_duration = 0.3f;  // 計時的總時間
    private void CoolDown_Gethit()
    {
        // 更新計時器
        Gethit_timer += Time.deltaTime;

        // 檢查是否超過了指定的時間
        if (Gethit_timer >= Gethit_duration)
        {
            Gethit = false;

            // 重置計時器
            Gethit_timer = 0f;
        }
    }





    private void MoveLikeTopDown()
    {
        //onGround
        onGround = Physics.CheckSphere(GroundChecker.position, checkRadius, layerMask);
        if (onGround && velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (onGround && Input.GetButtonDown("Jump") && Dizzy == false)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }




        //movement
        if(Dizzy == true)
        {
            speed = 0f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 6f;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            speed = 3f;
        }
        else
        {
            speed = 0f;
        }

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");     
        
        Vector3 face = transform.forward;
        var move = new Vector3(vertical*face.x + horizontal*face.z,0,vertical* face.z - horizontal * face.x);
        move = move * speed * Time.deltaTime;
        characterController.Move(move);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);  
        
        float mouseX = Input.GetAxis("Mouse X");

        // Calculate the rotation angle based on mouse movement
        float rotationAngle = mouseX * rotationSpeed;

        // Rotate the player around the y-axis
        transform.Rotate(Vector3.up, rotationAngle);




        //attack
        if(Input.GetMouseButtonDown(0) && Attacking == false && Dizzy == false)
        {
            Attacking = true;
            
            attack_anictrl = Random.Range(1, 3); //1 or 2
        }




        //Defense
        if (Input.GetMouseButton(1) && speed == 0 && Dizzy == false)
        {
            Defending = true;
        }
        else
        {
            Defending = false;
        }




        //Die
        if(true)
        {
            //Die = true;
        }




        //Dizzy
        if(Input.GetKeyDown(KeyCode.T))
        {
            Dizzy = true;
        }




        if(Global_Gethit == true)
        {
            Gethit = true;
            Global_Gethit = false;
        }
    }

    private void AnimatorControl()
    {
        animator.SetFloat("movingSpeed", speed);
        if (onGround == false)
        {
            //jumping
            animator.SetBool("onGround", false);
        }
        else
        {
            //onGround
            animator.SetBool("onGround", true);
        }

        //attack
        if (attack_anictrl == 1)
        {
            animator.SetInteger("Attack", attack_anictrl);
        }
        else if(attack_anictrl == 2)
        {
            animator.SetInteger("Attack", attack_anictrl);
        }
        else
        {
            animator.SetInteger("Attack", attack_anictrl);
        }

        //defense
        if(Defending == true)
        {
            animator.SetBool("Defend", true);
        }
        else
        {
            animator.SetBool("Defend", false);
        }

        //die
        if(Die == true)
        {
            animator.SetBool("Die", true);
        }

        //dizzy
        if (Dizzy == true)
        {
            animator.SetBool("Dizzy", true);
        }
        else
        {
            animator.SetBool("Dizzy", false);
        }

        //gethit
        if (Gethit == true)
        {
            animator.SetBool("Gethit", true);
        }
        else
        {
            animator.SetBool("Gethit", false);
        }
    }






    //========================================================
    //====================Global Get Value====================
    //========================================================
    public bool Get_Attacking()
    {
        return Attacking;
    }
    public void Receive_Attacking(bool Receive_Attacking)
    {
        Attacking = Receive_Attacking;
    }



    public bool Get_Defending()
    {
        return Defending;
    }
    public void Receive_Defending(bool Receive_Defending)
    {
        Defending = Receive_Defending;
    }



    public void Receive_Die(bool Receive_Die)
    {
        Defending = Receive_Die;
    }

    public bool Get_Gethit()
    {
        return Global_Gethit;
    }
    public void Receive_Gethit(bool Receive_Gethit)
    {
        Global_Gethit = Receive_Gethit;
    }
}
