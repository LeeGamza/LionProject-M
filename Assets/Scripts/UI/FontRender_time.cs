using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FontRender_time : MonoBehaviour
{
    
    public Transform parentContainer; 
    public Sprite[] numberSprites; 
    public float spaceWidth = 50f;
    public float fontSize = 50f;

    private int CurrentTime;

    void Start()
    {
        StartCoroutine(DecreaseTime());
    }

    IEnumerator DecreaseTime()
    {
        while (GameManager.Instance == null)
        {
            yield return null; // GameManager가 초기화될 때까지 대기
        }

        CurrentTime = GameManager.Instance.GetTimeLeft() / 4;

        while (CurrentTime > 0)
        {
            RenderTextImage(CurrentTime.ToString());
            yield return new WaitForSeconds(4f);
            CurrentTime--;
        }
    }

    public void RenderTextImage(string text)
    {

        foreach (Transform child in parentContainer)
        {
            Destroy(child.gameObject);
        }

        // 문자열을 한 글자씩 처리
        foreach (char c in text.ToUpper()) 
        {
            if (c == ' ') // 공백 처리
            {
                CreateSpace();
            }

            else if (c >= '0' && c <= '9') // 숫자 처리
            {
                int index = (c - '0');
                CreateLetterImage(numberSprites, index , c.ToString());
            }
        }
    }

    void CreateLetterImage(Sprite[] spriteArray, int index, string name)
    {
        if (index >= 0 && index < spriteArray.Length)
        {
            GameObject letterObj = new GameObject(name, typeof(Image));
            letterObj.transform.SetParent(parentContainer, false);

            Image img = letterObj.GetComponent<Image>();
            img.sprite = spriteArray[index];

            RectTransform rect = letterObj.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(fontSize, fontSize); 
        }
    }

    void CreateSpace()
    {
        GameObject spaceObj = new GameObject("Space");
        spaceObj.transform.SetParent(parentContainer, false);

        RectTransform rect = spaceObj.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(spaceWidth, 50); 
    }
}
