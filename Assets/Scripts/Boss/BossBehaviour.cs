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

        //Debug.Log("DeployingDeathRazer �ð� : " + GetAnimationLength(DDRAnimator, "DeployingDeathRazer"));
        //Debug.Log("DeathRazerOn �ð� : " + GetAnimationLength(DDRAnimator, "DeathRazerOn"));
        //Debug.Log("DeathRazerAttacking �ð� : " + GetAnimationLength(DDRAnimator, "DeathRazerAttacking"));
        //Debug.Log("DeployingIdle �ð� : " + GetAnimationLength(DDRAnimator, "DeployingIdle"));
        //Debug.Log("UndeployingDeathRazer �ð� : " + GetAnimationLength(DDRAnimator, "UndeployingDeathRazer"));
        Debug.Log("HatchingUFO �ð� : " + GetAnimationLength(HUFOAnimator, "HatchingUFO"));

        intervalTime = 2.0f;
        StartCoroutine(BossSkillBehavior());
        
    }

    IEnumerator BossSkillBehavior()
    {
        while(isAlive) //����ִٸ� ���ѹݺ�
        {
            //������ ��ų �ð���ŭ ���
            Debug.Log("��ų ���ð� : " + intervalTime);
            yield return new WaitForSeconds(intervalTime); //�ʱⰪ�� 2��

            //���� ��ų �ߵ�
            UseRandomSkill();
        }
    }

    private void UseRandomSkill()
    {
        int randomSkill = Random.Range(1, 11); //1 ~ 10������
        if(randomSkill <= 7)
        {
            StartCoroutine(DeployingDeathRazer()); //70% Ȯ���� DeployingDeathRazer
        }
        else
        {
            StartCoroutine(SpawnUFO()); //������ Ȯ��� UFO ��ȯ
        }
    }

    IEnumerator DeployingDeathRazer()
    {
        Debug.Log("DeployingDeathRazer!!");

        int randomCount = Random.Range(1, 5); //DeathRazer �߻� Ƚ��
        Debug.Log("���� ������ Ƚ�� : " + randomCount);
        intervalTime = randomCount * (GetAnimationLength(DDRAnimator, "DeathRazerOn") + GetAnimationLength(DDRAnimator, "DeathRazerAttacking") + GetAnimationLength(DDRAnimator, "DeployingIdle")) 
            + GetAnimationLength(DDRAnimator, "DeployingDeathRazer") + GetAnimationLength(DDRAnimator, "DeployingIdle")+ GetAnimationLength(DDRAnimator, "UndeployingDeathRazer") + 2.0f;
        
        //DeployingDeathRazer ������Ʈ Ű��
        DeployingDeathRazerObject.SetActive(true);
        yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeployingDeathRazer")); //DeployingDeathRazer �ִϸ��̼� ���� �ð� ��ŭ ��ũ��Ʈ ���
        while (randomCount > 0)
        {
            
            yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeathRazerOn")); //DeathRazerOn �ִϸ��̼� ���� �ð���ŭ ���
            Debug.Log("�߻� " + randomCount);
            randomCount--;
            DDRAnimator.SetBool("Attacking", true); //DeathRazerAttacking �ִϸ��̼� ���� �� ShootingDeathRazer �̺�Ʈ �߻�(DeathRazer ������ ���� �Լ� ����)
            yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeathRazerAttacking")); //DeathRazerAttacking ���� �ð� ��ŭ ���
            DDRAnimator.SetBool("Attacking", false); //DeployingIdle �ִϸ��̼�
            yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeployingIdle")); //DeployingIdle �ִϸ��̼� ����ð� ��ŭ ���
        }
        yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "DeployingIdle")); //DeployingIdle �ִϸ��̼� ����ð� ��ŭ ���
        DDRAnimator.SetTrigger("Undeploying");
        Debug.Log("Undeploying");
        yield return new WaitForSeconds(GetAnimationLength(DDRAnimator, "UndeployingDeathRazer")); //UndeployingDeathRazer �ִϸ��̼� ����ð���ŭ ���
        DeployingDeathRazerObject.SetActive(false);

    }

    // �ʿ� miniUFO�� ��� �ִ��� Ȯ���ϰ�, �ִ� 3������ �� �� ���� miniUFO ����... �߰�����
    // HatchingUFO ������Ʈ�� n�� ���� �״� => SetActive �� �� ���� UFO ������
    // (���� ����� ������ DaimanjiBottom���� HatchingUFO �ִϸ��̼��� ������ �� ������, ��������Ʈ �ȼ� ���߱Ⱑ ��ٷο��� �̷��� ����... ���̸� ħ���ؼ�)
    IEnumerator SpawnUFO()
    {
        Debug.Log("UFO ����");
        intervalTime = GetAnimationLength(HUFOAnimator, "HatchingUFO") + 2.0f;
        HatchingUFOObject.SetActive(true);
        yield return new WaitForSeconds(GetAnimationLength(HUFOAnimator, "HatchingUFO"));
        HatchingUFOObject.SetActive(false);

    }

    //Ư�� �ִϸ����Ϳ� ���Ե� �ִϸ��̼� Ŭ�� ����ð��� ��ȯ�ϴ� �Լ�
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
        //���̸��� ����
        Destroy(this.gameObject.transform.GetChild(1).gameObject); //1�� �ε����� �ִ°� ���̸��� ������Ʈ
        //���׳��ӿ� ������ �Ͼ�� ȿ�� ���̱�
    }

    //void ShootDeathRazer()
    //{
    //    // 1 ~ 3���� �������� Death�������� �� Ƚ�� ����
    //    int count = Random.Range(1, 5); //DeathRazer �߻� Ƚ��
    //    Debug.Log("���� ������ Ƚ�� : " + count);

    //    intervalTime = count * 5.5f + 3.5f;

    //    while (count > 0)
    //    {
    //        //yield return new WaitForSeconds(1.0f);
    //        Debug.Log("�߻� " + count);
    //        GameObject go = Instantiate(DeathRazerPrefab, new Vector3(this.transform.position.x - 0.1f, this.transform.position.y - 1.48f, 0), Quaternion.identity); //���׳��� ���� ��ġ ����
    //        count--;
    //        //yield return new WaitForSeconds(4.0f); //DeathRazer lifeTime��ŭ ��ٸ���
    //    }
    //}
}
