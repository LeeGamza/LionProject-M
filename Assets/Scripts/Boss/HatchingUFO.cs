using UnityEngine;

public class HatchingUFO : MonoBehaviour
{
    public GameObject MiniUFOPrefab;
    public Transform bossTransform;
    void Start()
    {
        
    }

    void SpawnUFO() //Hatching UFO �ִϸ��̼� â�� �� �� �����ӿ��� ȣ���� �Լ�
    {
        GameObject miniUFO = Instantiate(MiniUFOPrefab, new Vector3(bossTransform.position.x - 0.008f, bossTransform.position.y - 1.138f, 0), Quaternion.identity); //���̸��� ���� ��ġ ����
    }
}
