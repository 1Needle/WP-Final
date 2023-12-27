using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Character
{
    [SerializeField] PlayerController playerController;

    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(true) // GetHit
        {
            //Hurt(1); //Hurt
        }
    }

    public override void Hurt(float damage)
    {
        playerController.Receive_Gethit(true);

        hp -= damage;
    }
}
