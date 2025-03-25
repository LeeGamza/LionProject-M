using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public GameObject DeployingDeathRazerObject;
    public GameObject HatchingUFOObject;

    public Animator DDRAnimator;
    public Animator HUFOAnimator;

    public float intervalTime = 4.0f;
    public int hp = 300;
    public bool isAlive = true;
    void Start()
    {

        //Debug.Log("DeployingDeathRazer 시간 : " + GetAnimationLength(DDRAnimator, "DeployingDeathRazer"));
        //Debug.Log("DeathRazerOn 시간 : " + GetAnimationLength(DDRAnimator, "DeathRazerOn"));
        //Debug.Log("DeathRazerAttacking 시간 : " + GetAnimationLength(DDRAnimator, "DeathRazerAttacking"));
        //Debug.Log("DeployingIdle 시간 : " + GetAnimationLength(DDRAnimator, "DeployingIdle"));
        //Debug.Log("UndeployingDeathRazer 시간 : " + GetAnimationLength(DDRAnimator, "UndeployingDeathRazer"));
        Debug.Log("HatchingUFO 시간 : " + GetAnimationLength(HUFOAnimator, "HatchingUFO"));

        intervalTime = 2.0f;
        StartCoroutine(BossSkillBehavior());
        
    }

    IEnumerator BossSkillBehavior()
    {
        while(isAlive) //살아있다면 무한반복
        {
            //각각의 스킬 시간만큼 대기
            Debug.Log("스킬 대기시간 : " + intervalTime);
            yield return new WaitForSeconds(intervalTime); //초기값은 2초

            //랜덤 스킬 발동
            UseRandomSkill();
        }
    }

    private void UseRandomSkill()
    {
        int randomSkill = Random.Range(1, 11); //1 ~ 10번까지
        if(randomSkill <= 7)
        {
            StartCoroutine(DeployingDeathRazer()); //70% 확률로 DeployingDeathRazer
        }
        else
        {
            StartCoroutine(SpawnUFO()); //나머지 확룰로 UFO 소환
        }
    }

    IEnumerator DeployingDeathRazer()
    {
        Debug.Log("DeployingDeathRazer!!");

        int randomCount = Random.Range(1, 5); //DeathRazer 발사 횟수
        Debug.Log("뽑은 레이저 횟수 : " + randomCount);
        intervalTime = randomCount * (GetAnimationLength(DDRAnimator, "DeathRazerOn") + GetAnimationLength(DDRAnimator, "DeathRazerAttacking") + GetAnimationLength(DDRAnimator, "DeployingIdle")) 
            + GetAnimationLength(DDRAnimator, "DeployingDeathRazer") + GetAnimationLength(DDRAnimator, "DeployingIdle")+ GetAnimationLength(DDRAnimator, "UndeployingDeathRazer") + 2.0f;
        
        //DeployingDeathRazer 오브젝트 키기
        DeployingDeathRazerObject.SetActive(true);
        yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeployingDeathRazer")); //DeployingDeathRazer 애니메이션 실행 시간 만큼 스크립트 대기
        while (randomCount > 0)
        {
            
            yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeathRazerOn")); //DeathRazerOn 애니메이션 실행 시간만큼 대기
            Debug.Log("발사 " + randomCount);
            randomCount--;
            DDRAnimator.SetBool("Attacking", true); //DeathRazerAttacking 애니메이션 실행 및 ShootingDeathRazer 이벤트 발생(DeathRazer 프리팹 생성 함수 실행)
            yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeathRazerAttacking")); //DeathRazerAttacking 실행 시간 만큼 대기
            DDRAnimator.SetBool("Attacking", false); //DeployingIdle 애니메이션
            yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeployingIdle")); //DeployingIdle 애니메이션 실행시간 만큼 대기
        }
        yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeployingIdle")); //DeployingIdle 애니메이션 실행시간 만큼 대기
        DDRAnimator.SetTrigger("Undeploying");
        Debug.Log("Undeploying");
        yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "UndeployingDeathRazer")); //UndeployingDeathRazer 애니메이션 실행시간만큼 대기
        DeployingDeathRazerObject.SetActive(false);

    }

    // 맵에 miniUFO가 몇마리 있는지 확인하고, 최대 3마리가 될 때 까지 miniUFO 스폰... 추가예정
    // HatchingUFO 오브젝트를 n번 껏다 켰다 => SetActive 될 때 마다 UFO 스폰됨
    // (위와 비슷한 구조로 DaimanjiBottom에서 HatchingUFO 애니메이션을 구현할 수 있지만, 스프라이트 픽셀 맞추기가 까다로워서 이렇게 진행... 높이를 침범해서)
    IEnumerator SpawnUFO()
    {
        Debug.Log("UFO 스폰");
        intervalTime = GetAnimationLength(HUFOAnimator, "HatchingUFO") + 2.0f;
        HatchingUFOObject.SetActive(true);
        yield return new WaitForSeconds(GetAnimationLength(HUFOAnimator, "HatchingUFO"));
        HatchingUFOObject.SetActive(false);

    }

    //특정 애니메이터에 포함된 애니메이션 클립 실행시간을 반환하는 함수
    float GetAnimationLength(Animator animator, string animationName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0;
    }

    void TakeDamage()
    {

    }

    void Die()
    {
        //다이만지 삭제
        Destroy(this.gameObject.transform.GetChild(1).gameObject); //1번 인덱스에 있는게 다이만지 오브젝트
        //러그네임에 폭발이 일어나는 효과 붙이기
    }

    //void ShootDeathRazer()
    //{
    //    // 1 ~ 3까지 랜덤으로 Death레이저를 쏠 횟수 지정
    //    int count = Random.Range(1, 5); //DeathRazer 발사 횟수
    //    Debug.Log("뽑은 레이저 횟수 : " + count);

    //    intervalTime = count * 5.5f + 3.5f;

    //    while (count > 0)
    //    {
    //        //yield return new WaitForSeconds(1.0f);
    //        Debug.Log("발사 " + count);
    //        GameObject go = Instantiate(DeathRazerPrefab, new Vector3(this.transform.position.x - 0.1f, this.transform.position.y - 1.48f, 0), Quaternion.identity); //러그네임 기준 위치 설정
    //        count--;
    //        //yield return new WaitForSeconds(4.0f); //DeathRazer lifeTime만큼 기다리기
    //    }
    //}
}
