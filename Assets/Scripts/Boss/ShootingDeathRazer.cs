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
        // 1 ~ 3까지 랜덤으로 Death레이저를 쏠 횟수 지정
        //int count = Random.Range(1, 5); //DeathRazer 발사 횟수
        //Debug.Log("뽑은 레이저 횟수 : " + count);

        //intervalTime = count * 5.5f + 3.5f;

        //while (count > 0)
        //{
            //yield return new WaitForSeconds(1.0f);
            //Debug.Log("발사 " + count);
            GameObject deathRazer = Instantiate(DeathRazerPrefab, new Vector3(bossTransform.position.x, bossTransform.position.y - 1.368f, 0), Quaternion.identity); //다이만지 기준 위치 설정
            //count--;
            //yield return new WaitForSeconds(4.0f); //DeathRazer lifeTime만큼 기다리기
        //}
    }
}
