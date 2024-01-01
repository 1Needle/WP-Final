using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    // functions
    public abstract void Hurt(float damage);
    public abstract void Fire_Hurt(float damage, float last_time);
    public GameObject GetPrefabRoot()
    {
        return gameObject;
    }
}
