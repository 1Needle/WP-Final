using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SkillExplosion : MonoBehaviour
{
    [SerializeField] Transform This;
    [SerializeField] GameObject Particle_Explosion;
    [SerializeField] GameObject Particle_BIG_Explosion;

    [SerializeField] SphereCollider sphereCollider;

    private Character character;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Exist == true)
        {
            CoolDown_Exist();
        }
    }

    private bool Exist = false;
    private float Exist_timer = 0f;  // �p�ɾ��ܼ�
    private float Exist_duration = 0.1f;  // �p�ɪ��`�ɶ�
    private void CoolDown_Exist()
    {
        // ��s�p�ɾ�
        Exist_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (Exist_timer >= Exist_duration)
        {
            Exist = false;
            sphereCollider.enabled = false;

            // ���m�p�ɾ�
            Exist_timer = 0f;
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

            character.Hurt(60);

            Instantiate(Particle_Explosion, This.position, Quaternion.identity);
        }
    }

    public void Set_Exist()
    {
        Exist = true;
        sphereCollider.enabled = true;
        Instantiate(Particle_Explosion, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
        Instantiate(Particle_BIG_Explosion, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
    }
}
