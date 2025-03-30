using System.Collections;
using UnityEngine;

public class CompleteMission : MonoBehaviour
{
    private GameObject boss;
    private bool isBossStage = false;
    private bool isGameEnded = false;
    public GameObject endingPanel; 

    void Start()
    {
        InvokeRepeating("CheckForBoss", 0f, 1f);
    }

    void Update()
    {
        if (isGameEnded) return;

        if (isBossStage && (boss == null || !boss.activeSelf))
        {
            BossDefeated();
        }
    }

    void CheckForBoss()
    {
        if (boss != null || isGameEnded) return;

        boss = GameObject.FindWithTag("Boss");
        if (boss != null)
        {
            isBossStage = true;
        }
    }

    void BossDefeated()
    {

        isGameEnded = true;
        endingPanel.SetActive(true);
        StartCoroutine(LoadEndingScene());
    }
    
    private IEnumerator LoadEndingScene()
    {
        yield return new WaitForSeconds(3f); // 3초 대기
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndingScene");
    }



}