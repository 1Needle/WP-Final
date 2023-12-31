using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform Particle_SwordFire;
    [SerializeField] Transform Particle_Buff;
    [SerializeField] Transform Particle_OnHeal;
    [SerializeField] Transform Particle_EasterEggs_Rocket;

    [SerializeField] AudioController audioController;

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

    //Skills
    private bool skills_OnFire = false;
    private bool skills_OnHeal = false;

    private bool EasterEggs_Rocket = false;

    //Global
    [SerializeField] PlayerData playerData;
    private bool Global_Gethit = false;

    [SerializeField] TextMeshProUGUI textMeshPro_OnFire;
    [SerializeField] TextMeshProUGUI textMeshPro_OnHeal;

    /*//Audio
    private bool audio_Walk;
    private bool audio_Hit = false;
    private bool audio_GetHit = false;
    private bool audio_Defending_GetHit = false;*/

    // Start is called before the first frame update
    void Start()
    {
        characterController = transform.GetComponent<CharacterController>();
        animator = transform.GetComponent<Animator>();
        playerData = transform.GetComponent<PlayerData>();

        textMeshPro_OnFire.text = "";
        textMeshPro_OnHeal.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Die == true)
        {
            AnimatorControl();
            return;
        }
        if(Attacking == true) 
        {
            CoolDown_Attack();
        }
        if(attack_anictrl != 0)
        {
            CoolDown_aniAttack();
        }
        if(Dizzy == true) 
        {
            CoolDown_Dizzy();
        }
        if(Gethit == true)
        {
            CoolDown_Gethit();
        }

        //Skills
        if(skills_OnFire == true)
        {
            Skilling_Skills_OnFire();
        }
        if(skills_OnFire_IsCD == true)
        {
            
            CoolDown_Skills_OnFire();
        }
        if (skills_OnHeal == true)
        {
            Skilling_Skills_OnHeal();
        }
        if (skills_OnHeal_IsCD == true)
        {

            CoolDown_Skills_OnHeal();
        }


        MoveLikeTopDown();
        AnimatorControl();
        ParticleControl();
    }

    private float Attack_timer = 0f;  // �p�ɾ��ܼ�
    private float Attack_duration = 1f;  // �p�ɪ��`�ɶ�
    private void CoolDown_Attack()
    {
        // ��s�p�ɾ�
        Attack_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (Attack_timer >= Attack_duration)
        {
            Attacking = false;

            // ���m�p�ɾ�
            Attack_timer = 0f;
        }
    }
    private float aniAttack_timer = 0f;  // �p�ɾ��ܼ�
    private float aniAttack_duration = 0.25f;  // �p�ɪ��`�ɶ�
    private void CoolDown_aniAttack()
    {
        // ��s�p�ɾ�
        aniAttack_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (aniAttack_timer >= aniAttack_duration)
        {
            attack_anictrl = 0;

            // ���m�p�ɾ�
            aniAttack_timer = 0f;
        }
    }


    private float Dizzy_timer = 0f;  // �p�ɾ��ܼ�
    private float Dizzy_duration = 5f;  // �p�ɪ��`�ɶ�
    private void CoolDown_Dizzy()
    {
        // ��s�p�ɾ�
        Dizzy_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (Dizzy_timer >= Dizzy_duration)
        {
            Dizzy = false;

            // ���m�p�ɾ�
            Dizzy_timer = 0f;
        }
    }

    private float Gethit_timer = 0f;  // �p�ɾ��ܼ�
    private float Gethit_duration = 0.3f;  // �p�ɪ��`�ɶ�
    private void CoolDown_Gethit()
    {
        // ��s�p�ɾ�
        Gethit_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (Gethit_timer >= Gethit_duration)
        {
            Gethit = false;

            // ���m�p�ɾ�
            Gethit_timer = 0f;
        }
    }

    private bool skills_OnFire_IsCD = false;
    private float Skills_OnFire_timer = 0f;  // �p�ɾ��ܼ�
    private float Skills_OnFire_duration = 10f;  // �p�ɪ��`�ɶ�
    private void Skilling_Skills_OnFire()
    {
        // ��s�p�ɾ�
        Skills_OnFire_timer += Time.deltaTime;
        playerData.Receive_dmg("OnFire");

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (Skills_OnFire_timer >= Skills_OnFire_duration)
        {
            skills_OnFire = false;
            playerData.Receive_dmg("Stop_OnFire");

            // ���m�p�ɾ�
            Skills_OnFire_timer = 0f;
        }
    }
    private float CD_Skills_OnFire_timer = 0f;  // �p�ɾ��ܼ�
    private float CD_Skills_OnFire_duration = 30f - 10f;  // �p�ɪ��`�ɶ�
    private void CoolDown_Skills_OnFire()
    {
        // ��s�p�ɾ�
        CD_Skills_OnFire_timer += Time.deltaTime;
        textMeshPro_OnFire.text = $"{30 - 10 - (int)CD_Skills_OnFire_timer}"; 

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (CD_Skills_OnFire_timer >= CD_Skills_OnFire_duration)
        {
            skills_OnFire_IsCD = false;
            textMeshPro_OnFire.text = "";

            // ���m�p�ɾ�
            CD_Skills_OnFire_timer = 0f;
        }
    }

    private bool skills_OnHeal_IsCD = false;
    private float Skills_OnHeal_timer = 0f;  // �p�ɾ��ܼ�
    private float Skills_OnHeal_duration = 10f;  // �p�ɪ��`�ɶ�
    private void Skilling_Skills_OnHeal()
    {
        // ��s�p�ɾ�
        Skills_OnHeal_timer += Time.deltaTime;
        playerData.Get_Heal(0.01f);


        // �ˬd�O�_�W�L�F���w���ɶ�
        if (Skills_OnHeal_timer >= Skills_OnHeal_duration)
        {
            skills_OnHeal = false;

            // ���m�p�ɾ�
            Skills_OnHeal_timer = 0f;
        }
    }
    private float CD_Skills_OnHeal_timer = 0f;  // �p�ɾ��ܼ�
    private float CD_Skills_OnHeal_duration = 30f - 10f;  // �p�ɪ��`�ɶ�
    private void CoolDown_Skills_OnHeal()
    {
        // ��s�p�ɾ�
        CD_Skills_OnHeal_timer += Time.deltaTime;
        textMeshPro_OnHeal.text = $"{30 - 10 - (int)CD_Skills_OnHeal_timer}";

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (CD_Skills_OnHeal_timer >= CD_Skills_OnHeal_duration)
        {
            skills_OnHeal_IsCD = false;
            textMeshPro_OnHeal.text = "";

            // ���m�p�ɾ�
            CD_Skills_OnHeal_timer = 0f;
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
        if(move.x != 0 && speed == 3 && onGround == true)
        {
            audioController.Receive_Audio_Walk();
        }
        else
        {
            audioController.StopLooping_Walk();
        }

        if (move.x != 0 && speed == 6 && onGround == true)
        {
            audioController.Receive_Audio_Run();
        }
        else
        {
            audioController.StopLooping_Run();
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);  
        
        float mouseX = Input.GetAxis("Mouse X");

        // Calculate the rotation angle based on mouse movement
        float rotationAngle = mouseX * rotationSpeed;

        // Rotate the player around the y-axis
        transform.Rotate(Vector3.up, rotationAngle);




        //attack
        if(Input.GetMouseButtonDown(0) && Attacking == false && Dizzy == false && Defending != true && onGround == true && speed != 6)
        {
            Attacking = true;
            
            attack_anictrl = Random.Range(1, 3); //1 or 2
            audioController.Receive_Audio_Hit();
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




        //Gethit
        if(Global_Gethit == true)
        {
            Gethit = true;
            Global_Gethit = false;
            audioController.Receive_Audio_GetHit();
        }




        //Skills
        //OnFire
        if (Input.GetKeyDown(KeyCode.Alpha1) && skills_OnFire_IsCD == false)
        {
            playerData.Receive_dmg("OnFire");
            skills_OnFire = true;
            skills_OnFire_IsCD = true;
            audioController.Receive_Audio_Skill("OnFire");
        }
        

        //OnHeal
        if(Input.GetKeyDown(KeyCode.Alpha2) && skills_OnHeal_IsCD == false)
        {
            skills_OnHeal = true;
            skills_OnHeal_IsCD = true;
            audioController.Receive_Audio_Skill("OnHeal");
        }

        //EasterEggs
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(EasterEggs_Rocket == false)
            {
                EasterEggs_Rocket = true;
            }
            else
            {
                EasterEggs_Rocket = false;
            }
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





    private void ParticleControl()
    {
        //Skills
        //OnFire
        if (skills_OnFire == true)
        {
            Particle_SwordFire.gameObject.SetActive(true);
            Particle_Buff.gameObject.SetActive(true);
        }
        else
        {
            Particle_SwordFire.gameObject.SetActive(false);
            Particle_Buff.gameObject.SetActive(false);
        }

        //OnHeal
        if (skills_OnHeal == true)
        {
            Particle_OnHeal.gameObject.SetActive(true);
        }
        else
        {
            Particle_OnHeal.gameObject.SetActive(false);
        }

        //Rocket
        if (EasterEggs_Rocket == true)
        {
            Particle_EasterEggs_Rocket.gameObject.SetActive(true);
        }
        else
        {
            Particle_EasterEggs_Rocket.gameObject.SetActive(false);
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
        Die = Receive_Die;
    }

    public bool Get_Gethit()
    {
        return Global_Gethit;
    }
    public void Receive_Gethit(bool Receive_Gethit)
    {
        Global_Gethit = Receive_Gethit;
    }

    public bool Get_Dizzy()
    {
        return Dizzy;
    }
    public void Receive_Dizzy(bool Receive_Dizzy)
    {
        Dizzy = Receive_Dizzy;
    }

    public bool Get_OnFire()
    {
        return skills_OnFire;
    }
}
