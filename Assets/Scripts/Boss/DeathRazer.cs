using UnityEditor.Rendering;
using UnityEngine;

public class DeathRazer : MonoBehaviour
{
    //생성시 콜리더 0.5초후 셋업되게, 

    public Transform playerTransform;
    Vector3 dir;
    public float speed;
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        dir = new Vector3(playerTransform.position.x, playerTransform.position.y, 0) - new Vector3(this.transform.position.x, this.transform.position.y, 0); //보스에서 플레이어쪽을 바라보는 방향벡터
        dir.Normalize(); //크기 1 방향벡터로 정규화
        //스피드는 애니메이션 속도로 조절
    }

    void Update()
    {
        this.transform.Translate(dir * speed * Time.deltaTime);
    }

    void RazerBody()
    {
        //레이저 바디 스폰
    }
}
