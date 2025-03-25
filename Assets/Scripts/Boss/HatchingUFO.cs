using UnityEngine;

public class HatchingUFO : MonoBehaviour
{
    public GameObject MiniUFOPrefab;
    public Transform bossTransform;
    void Start()
    {
        
    }

    void SpawnUFO() //Hatching UFO 애니메이션 창의 맨 끝 프레임에서 호출할 함수
    {
        GameObject miniUFO = Instantiate(MiniUFOPrefab, new Vector3(bossTransform.position.x - 0.008f, bossTransform.position.y - 1.138f, 0), Quaternion.identity); //다이만지 기준 위치 설정
    }
}
