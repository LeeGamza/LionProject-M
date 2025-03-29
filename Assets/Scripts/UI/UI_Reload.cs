using UnityEngine;

public class UI_Reload : MonoBehaviour
{
    public GameObject uiPrefab;
    private GameObject currentUI;


    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetUI();
        }
    }

    // UI를 제거하고 새로 생성
    private void ResetUI()
    {
        if (currentUI != null)
        {
            Destroy(currentUI);
        }

        SpawnUI();
    }

    // UI 생성
    private void SpawnUI()
    {
        if (uiPrefab != null)
        {
            currentUI = Instantiate(uiPrefab, transform);
            Debug.Log("새 UI가 생성되었습니다.");
        }
        else
        {
            Debug.LogError("UI 프리팹이 설정되지 않았습니다!");
        }
    }
}

