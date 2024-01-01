using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Image healthbar;
    [SerializeField] float smoothCoefficient;
    Camera cam;
    float target = 1;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, target, smoothCoefficient);
    }

    public void UpdateHealthbar(float percentage)
    {
        target = percentage;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
