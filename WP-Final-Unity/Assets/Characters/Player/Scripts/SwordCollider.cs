using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] CapsuleCollider capsuleCollider;

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
            SwordCollision =false;
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
        if (other.CompareTag("Enemy"))
        {
            // �b�o�̰���P�I����H�������ާ@
            Debug.Log("Sword Collide with: Enemy");

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
