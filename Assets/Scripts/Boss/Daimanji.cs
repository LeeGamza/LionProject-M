using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Daimanji : Monster
{
    [SerializeField] protected new float hp;

    [SerializeField] protected new float moveSpeed = 1f;

    public GameObject HatchingUFOObject;
    public Animator HUFOAnimator;
    private static int currentMiniUFOCount;
    public float intervalTime = 2.0f;
    void Start()
    {
        StartCoroutine(StateLoop());
    }

    void Update()
    {
        
    }
    IEnumerator StateLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalTime);
            yield return SpawnUFO();
        }
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
        intervalTime = (3 - currentMiniUFOCount) * GetAnimationLength(HUFOAnimator, "HatchingUFO") + 3.0f;
        //3마리가 될 때 까지 계속 생산
        while (currentMiniUFOCount < 3)
        {
            Debug.Log("UFO 스폰");
            currentMiniUFOCount++;
            HatchingUFOObject.SetActive(true);
            yield return new WaitForSeconds(GetAnimationLength(HUFOAnimator, "HatchingUFO"));
            HatchingUFOObject.SetActive(false);
            yield return new WaitForSeconds(2.0f); //천천히 생성되게
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

    protected override void Move()
    {

    }
    
}
