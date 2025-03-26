using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScatterUIImages : MonoBehaviour
{
    public Transform parentContainer; 
    public Transform targetPoint; 
    public float moveDistance = 300f; // 이동 거리
    public float moveDuration = 0.5f; // 이동 시간
    public float delay = 2f; // 2초 후 시작

    void Start()
    {
        StartCoroutine(StartScatter());
    }

    IEnumerator StartScatter()
    {
        yield return new WaitForSeconds(delay); 

        HorizontalLayoutGroup layoutGroup = parentContainer.GetComponent<HorizontalLayoutGroup>();
        if (layoutGroup != null) layoutGroup.enabled = false; 

        foreach (Transform child in parentContainer)
        {
            if (child.GetComponent<Image>() != null)
                StartCoroutine(MoveAndDestroy(child));
        }
    }

    IEnumerator MoveAndDestroy(Transform letter)
    {
        Vector3 targetPos = letter.position + (letter.position - targetPoint.position).normalized * moveDistance;
        Vector3 startPos = letter.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            letter.position = Vector3.Lerp(startPos, targetPos, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        letter.position = targetPos; 
        Destroy(letter.gameObject); 
    }
}
