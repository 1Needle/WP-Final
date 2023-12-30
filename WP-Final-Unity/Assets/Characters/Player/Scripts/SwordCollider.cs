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
        // 檢查碰撞的對象是否擁有指定的標籤
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Sword Collide with: Enemy");

            // 在這裡執行與碰撞對象相關的操作
            character = other.GetComponent<Character>();
            float dmg = playerData.Get_dmg();

            if(playerController.Get_OnFire() == true)
            {
                character.Fire_Hurt(dmg, 3);
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
