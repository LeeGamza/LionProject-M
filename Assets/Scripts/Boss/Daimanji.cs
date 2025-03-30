using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Daimanji : Monster
{
    public float outsideOffset = 2f; // 카메라 밖 위치 오프셋
    private Vector2 startPos;
    private Vector2 endPos;
    private Camera mainCamera;
    protected new float hp;

    public GameObject HatchingUFOObject;
    public Animator HUFOAnimator;
    private static int currentMiniUFOCount;
    public float intervalTime = 2.0f;

    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.red;

    private void Awake()
    {
        if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
        {
            Debug.LogError("Monster : UnFound SpriteRenderer", this);
        }
    }
    void Start()
    {
        moveSpeed = 1f;
        mainCamera = Camera.main;
        this.hp = 1000f;

        StartCoroutine(SkillLoop());
        StartCoroutine(MoveLoop());

    }

    IEnumerator SkillLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalTime);
            yield return SpawnUFO();
        }
    }
    IEnumerator MoveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            yield return MoveObject();
        }
    }
    IEnumerator MoveObject()
    {
        if (startPos == new Vector2(0, 0))
            startPos = GetPosOutside();
        else
            startPos = transform.position;
        endPos = GetPosInside();
        transform.position = startPos;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector2.Lerp(startPos, endPos, time);
            yield return null;
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


        Vector2 GetPosOutside()
        {
            Vector2 camPos = mainCamera.transform.position;
            float camSize = mainCamera.orthographicSize;
            float camAspect = mainCamera.aspect;

            float randomX = Random.Range(-camAspect * camSize - outsideOffset, camAspect * camSize + outsideOffset);
            float randomY = Random.Range(camSize / 2, camSize) + outsideOffset; // 절반 이상에서 생성

            return new Vector2(camPos.x + randomX, camPos.y + randomY);
        }

        Vector2 GetPosInside()
        {
            Vector2 camPos = mainCamera.transform.position;
            float camSize = mainCamera.orthographicSize;
            float camAspect = mainCamera.aspect;

            float randomX = Random.Range(-camAspect * camSize * 0.8f, camAspect * camSize * 0.8f);
            float randomY = Random.Range(-camSize * 0.1f, camSize);

            return new Vector2(camPos.x + randomX, camPos.y + randomY);
        }

        public override void Damaged(float damage)
        {
        Debug.Log("몬스터가 데미지를 받음: " + damage + ", 현재 HP: " + this.hp);
        this.hp -= damage;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSound);
        EventManager.Instance.InvokeHitEffect(spriteRenderer, hitColor);

        if (this.hp <= 0)
        {
            GameObject dieAnim = Instantiate(die, gameObject.transform.position, Quaternion.identity);
            Destroy(dieAnim, 4f);
            StopAllCoroutines();
            Invoke("Die", 4.0f);
        }

        }
}
