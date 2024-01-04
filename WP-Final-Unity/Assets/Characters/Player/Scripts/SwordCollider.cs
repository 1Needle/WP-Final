using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SwordCollider : MonoBehaviour
{
    [SerializeField] GameObject Particle_StarHit;
    [SerializeField] GameObject Particle_StoneHit;
    [SerializeField] Transform This;

    [SerializeField] PlayerController playerController;
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] PlayerData playerData;

    private Character character;

    private bool SwordCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool Attacking = playerController.Get_Attacking();
        if(Attacking == false)
        {
            SwordCollision = false;
            capsuleCollider.enabled = false;
        }
        if(Attacking == true && SwordCollision == false && thisAttacking == false)
        {
            thisAttacking = true;
            Delay_On = true;
            Collider_Closer = true;
        }

        if(Delay_On == true)
        {
            Delay_SwordCollider();
        }
        if(Collider_Closer == true)
        {
            Collider_Closer_SwordCollider();
        }
        if (thisAttacking == true)
        {
            thisAttacking_SwordCollider();
        }
    }

    private bool Delay_On = false;
    private float Delay_timer = 0f;  // �p�ɾ��ܼ�
    private float Delay_duration = 0.1f;  // �p�ɪ��`�ɶ�
    private void Delay_SwordCollider()
    {
        // ��s�p�ɾ�
        Delay_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (Delay_timer >= Delay_duration)
        {
            capsuleCollider.enabled = true;
            Delay_On = false;

            // ���m�p�ɾ�
            Delay_timer = 0f;
        }
    }

    private bool Collider_Closer = false;
    private float Collider_Closer_timer = 0f;  // �p�ɾ��ܼ�
    private float Collider_Closer_duration = 0.4f;  // �p�ɪ��`�ɶ�
    private void Collider_Closer_SwordCollider()
    {
        // ��s�p�ɾ�
        Collider_Closer_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (Collider_Closer_timer >= Collider_Closer_duration)
        {
            capsuleCollider.enabled = false;
            Collider_Closer = false;

            // ���m�p�ɾ�
            Collider_Closer_timer = 0f;
        }
    }

    private bool thisAttacking = false;
    private float thisAttacking_timer = 0f;  // �p�ɾ��ܼ�
    private float thisAttacking_duration = 1f;  // �p�ɪ��`�ɶ�
    private void thisAttacking_SwordCollider()
    {
        // ��s�p�ɾ�
        thisAttacking_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (thisAttacking_timer >= thisAttacking_duration)
        {
            thisAttacking = false;

            // ���m�p�ɾ�
            thisAttacking_timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �ˬd�I������H�O�_�֦����w������
        if (other.transform.root.CompareTag("Enemy"))
        {
            //Debug.Log("Sword Collide with: Enemy");

            // �b�o�̰���P�I����H�������ާ@
            character = other.GetComponent<Character>();
            if (character == null)
                character = other.GetComponentInParent<Character>();
            float dmg = playerData.Get_dmg();
            float Fire_dmg = playerData.Get_Fire_dmg();

            if (playerController.Get_OnFire() == true)
            {
                character.Fire_Hurt(Fire_dmg, 3);
            }

                //Debug.Log($"Player->Enemy: {dmg}");
                if(character.name == "Dragon")
                {
                    DragonScripts dragonScript = character.GetComponent<DragonScripts>();
                    dragonScript.PartDamage(dmg, other.tag);
                }
                else
                {
                    character.Hurt(dmg);
                }

            float left_explosion_CD = playerController.Get_Skills_Explosion_leftCD();
            if (left_explosion_CD > 0)
            {
                float rndnum = Random.Range(0.0f, 5.0f);
                playerController.Receive_Skills_Explosion_minus_CD(rndnum);
            }

            
            Instantiate(Particle_StarHit, This.position, Quaternion.identity);
            Instantiate(Particle_StoneHit, This.position, Quaternion.identity);

            //close collider to avoid multi-collision
            capsuleCollider.enabled = false;
            SwordCollision = true;
        }
    }







    //========================================================
    //====================Global Get Value====================
    //========================================================
    public bool Get_SwordCollision()
    {
        return SwordCollision;
    }
}
