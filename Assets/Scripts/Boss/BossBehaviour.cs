using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour // 보스(러그네임 + 다이만지) 행동패턴 스크립트
{
    public GameObject DeployingDeathRazerObject;
    public GameObject HatchingUFOObject;
    public GameObject RazerEffect;
    public GameObject Explosion;

    public Animator DDRAnimator;
    public Animator HUFOAnimator;

    public float intervalTime = 4.0f;
    public float hp = 300;
    public bool isAlive = true;

    GameObject daimanji;
    private SpriteRenderer daimanji_spriteRenderer;
    private Color hitColor = Color.red;


    private static int currentMiniUFOCount;

    private void Awake()
    {
        daimanji = transform.GetChild(1).gameObject; //1번 인덱스에있는 다이만지 가져옴
        if (!daimanji.TryGetComponent<SpriteRenderer>(out daimanji_spriteRenderer))
        {
            Debug.LogError("Monster : UnFound SpriteRenderer", this);
        }
    }
    void Start()
    {
        transform.DOMoveY(transform.position.y - 4, 2.0f);

        //transform.position = transform.position - new Vector3(0, 2f, 0);
        //Debug.Log("DeployingDeathRazer 시간 : " + GetAnimationLength(DDRAnimator, "DeployingDeathRazer"));
        //Debug.Log("DeathRazerOn 시간 : " + GetAnimationLength(DDRAnimator, "DeathRazerOn"));
        //Debug.Log("DeathRazerAttacking 시간 : " + GetAnimationLength(DDRAnimator, "DeathRazerAttacking"));
        //Debug.Log("DeployingIdle 시간 : " + GetAnimationLength(DDRAnimator, "DeployingIdle"));
        //Debug.Log("UndeployingDeathRazer 시간 : " + GetAnimationLength(DDRAnimator, "UndeployingDeathRazer"));
        //Debug.Log("HatchingUFO 시간 : " + GetAnimationLength(HUFOAnimator, "HatchingUFO"));

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
        if(randomSkill <= 6)
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

        int randomCount = Random.Range(1, 4); //DeathRazer 발사 횟수
        Debug.Log("뽑은 레이저 횟수 : " + randomCount);
        intervalTime = randomCount * (GetAnimationLength(DDRAnimator, "DeathRazerOn") + GetAnimationLength(DDRAnimator, "DeathRazerAttacking") + GetAnimationLength(DDRAnimator, "DeployingIdle")) 
            + GetAnimationLength(DDRAnimator, "DeployingDeathRazer") + GetAnimationLength(DDRAnimator, "DeployingIdle") + GetAnimationLength(DDRAnimator, "DeployingIdle") + GetAnimationLength(DDRAnimator, "UndeployingDeathRazer") + 2.0f;
        
        //DeployingDeathRazer 오브젝트 키기
        DeployingDeathRazerObject.SetActive(true);
        yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeployingDeathRazer")); //DeployingDeathRazer 애니메이션 실행 시간 만큼 스크립트 대기
        yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeployingIdle")); //DeployingIdle 애니메이션 실행 시간 만큼 스크립트 대기
        while (randomCount > 0)
        {
            DDRAnimator.SetBool("Attacking", true); //DeathRazerAttacking 애니메이션 실행 및 ShootingDeathRazer 이벤트 발생(DeathRazer 프리팹 생성 함수 실행)
            RazerEffect.SetActive(true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.DeathRazerOnSound);
            yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeathRazerOn")); //DeathRazerOn 애니메이션 실행 시간만큼 대기
            Debug.Log("발사 " + randomCount);
            randomCount--;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.DeathRazerSound);
            yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeathRazerAttacking")); //DeathRazerAttacking 실행 시간 만큼 대기
            DDRAnimator.SetBool("Attacking", false); //DeployingIdle 애니메이션
            RazerEffect.SetActive(false);
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
        //스킬이 발동하면 맵에 미니 UFO가 몇마리인지 탐지
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); //다이만지 씬에는 몬스터가 miniufo 뿐이라서 tag를 이용해 찾음
        currentMiniUFOCount = monsters.Length;
        Debug.Log("---현재 몬스터 수 : " + currentMiniUFOCount);
        intervalTime = (3 - currentMiniUFOCount) * GetAnimationLength(HUFOAnimator, "HatchingUFO") + 2.0f;
        //3마리가 될 때 까지 계속 생산
        while (currentMiniUFOCount < 3)
        {
            Debug.Log("UFO 스폰");
            currentMiniUFOCount++;
            HatchingUFOObject.SetActive(true);
            yield return new WaitForSeconds(GetAnimationLength(HUFOAnimator, "HatchingUFO"));
            HatchingUFOObject.SetActive(false);
        }

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

    public virtual void Damaged(float damage)
    {
        Debug.Log("보스가 데미지를 받음: " + damage + ", 현재 HP: " + hp);
        hp -= damage;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSound);
        EventManager.Instance.InvokeHitEffect(daimanji_spriteRenderer, hitColor);

        if (hp <= 0)
        {
            //러그네임에 폭발이 일어나는 효과 붙이기
            isAlive = false;
            Explosion.SetActive(true);
            Invoke("Die", 3.0f);
        }

    }

    void Die()
    {
        //다이만지 비활성화
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false); //1번 인덱스에 있는게 다이만지 오브젝트 비활성화
        Destroy(this); // 이 스크립트 삭제
       
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
