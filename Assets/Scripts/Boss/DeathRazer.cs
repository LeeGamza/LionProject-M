using UnityEditor.Rendering;
using UnityEngine;

public class DeathRazer : MonoBehaviour
{
    //������ �ݸ��� 0.5���� �¾��ǰ�, 

    public Transform playerTransform;
    Vector3 dir;
    public float speed;
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        dir = new Vector3(playerTransform.position.x, playerTransform.position.y, 0) - new Vector3(this.transform.position.x, this.transform.position.y, 0); //�������� �÷��̾����� �ٶ󺸴� ���⺤��
        dir.Normalize(); //ũ�� 1 ���⺤�ͷ� ����ȭ
        //���ǵ�� �ִϸ��̼� �ӵ��� ����
    }

    void Update()
    {
        this.transform.Translate(dir * speed * Time.deltaTime);
    }

    void RazerBody()
    {
        //������ �ٵ� ����
    }
}
