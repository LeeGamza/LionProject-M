using UnityEngine;

public class ShootingDeathRazer : MonoBehaviour
{
    public GameObject DeathRazerPrefab;
    //BossBehaviour bossBehaviour;
    public Transform bossTransform;
    void Start()
    {
        //bossBehaviour = GetComponentInParent<BossBehaviour>();
    }

    void ShootDeathRazer()
    {
        // 1 ~ 3���� �������� Death�������� �� Ƚ�� ����
        //int count = Random.Range(1, 5); //DeathRazer �߻� Ƚ��
        //Debug.Log("���� ������ Ƚ�� : " + count);

        //intervalTime = count * 5.5f + 3.5f;

        //while (count > 0)
        //{
            //yield return new WaitForSeconds(1.0f);
            //Debug.Log("�߻� " + count);
            GameObject deathRazer = Instantiate(DeathRazerPrefab, new Vector3(bossTransform.position.x, bossTransform.position.y - 1.368f, 0), Quaternion.identity); //���̸��� ���� ��ġ ����
            //count--;
            //yield return new WaitForSeconds(4.0f); //DeathRazer lifeTime��ŭ ��ٸ���
        //}
    }
}
