using System.Collections;
using UnityEngine;

public class DeathRazer : MonoBehaviour
{
    Transform playerTransform;
    BoxCollider2D boxCollider;

    private float duration = 1.0f; // Ȯ�忡 �ɸ��� �ð�
    private float maxHeight = 10.0f; // box collider size �ִ� y ����
    private float lifeTime = 4.0f; 

    private Vector2 originalSize; // ���� ũ�� ����
    private Vector2 originalOffset; // ���� ������ ����...�ڽ��ݸ����� �ø� �� �߾ӿ������� �� �Ʒ��� �÷����°� ���������� �Ʒ��θ� �÷����� �����ϵ���... 

    void Start()
    {
        StartCoroutine(LifeTime());
        try
        {
            if (!GameObject.FindWithTag("Player").TryGetComponent<Transform>(out playerTransform))
            {
                Debug.LogError("Cannot find Player Object in DeathRazer Script : ", this);
            }
        }
        catch
        {
            Debug.Log("DeathRazer 스크립트에서 Player 객체를 찾을 수 없음");
        }

        boxCollider = GetComponent<BoxCollider2D>();
        // ���� ũ��, ��ġ ����
        originalSize = boxCollider.size;
        originalOffset = boxCollider.offset;
        

        if (playerTransform != null)
        {
            //�÷��̾� �������� ������ ȸ�� (0~50��, 310~360�� ���� ����) 
            InitRotating();
            // ������ �ڿ������� �ݶ��̴��� �ø��� �ڷ�ƾ
            StartCoroutine(IncreaseColliderY());
            //���ӽð��� ������ �� ������Ʈ�� �������ִ� �ڷ�ƾ
        }
        
    }
    
    // ȣ�� �������� ���� �� collider�� �ڿ������� �÷������� ������ִ� �ڷ�ƾ
    IEnumerator IncreaseColliderY()
    {
        float elapsedTime = 0f; //��� �ð�

        while (elapsedTime < duration)
        {
            //��� �ð��� ���� y ���̸� ����
            float newHeight = Mathf.Lerp(0, maxHeight, elapsedTime / duration);
            boxCollider.size = new Vector2(originalSize.x, newHeight);

            //�ݶ��̴� offset�� ���ʿ������� �Ʒ��� Ȯ��ǵ���... �ٽ� ���ϱ� ��������Ʈ �ڸ� �� �ǹ��� ���ʿ� �θ� �̷��� �� �ʿ� ���� size�� ���������� �� ����
            boxCollider.offset = new Vector2(originalOffset.x, originalOffset.y - newHeight / 2);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    void InitRotating()
    {
        Vector3 dir = (playerTransform.position - this.transform.position); //�������� �÷��̾ �ٶ󺸴� ���⺤��
        dir.Normalize(); //ũ�� 1 ���⺤�ͷ� ����ȭ

        float angle = 180.0f - Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        //Debug.Log($"�ʱ� ���� : {angle}");
        //�� �� ���� 50�� �̻� ���Ѿ�� angle ���� ����
        if (angle > 50.0f && angle < 180.0f)
        {
            angle = 50.0f;
        }
        else if (angle > 180.0f && angle < 310.0f)
        {
            angle = 310.0f;
        }
        //Debug.Log($"���� ���� : {angle}");

        //transform.Rotate(Vector3.forward, angle); //Z���� �������� angle��ŭ ȸ�� (�÷��̾ �ٶ󺸴� ����)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            if (collision.GetComponentInParent<Player>() is { } player)
            {
                player.TakeDamage();
            }
        }
    }
}
