using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SwordCollider : MonoBehaviour
{
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
        if(Attacking == true && SwordCollision == false)
        {
            if(capsuleCollider.enabled == false)
            {
                capsuleCollider.enabled = true;
            }
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
                character.Fire_Hurt(dmg, 3);
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
