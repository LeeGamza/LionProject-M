using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FontRender_time : MonoBehaviour
{
    public string NumberToDisplay; 
    public Transform parentContainer; 
    public Sprite[] numberSprites; 
    public float spaceWidth = 50f;
    public float fontSize = 50f;

    private int CurrentTime;

    void Start()
    {
        CurrentTime = int.Parse(NumberToDisplay);
        RenderTextImage(NumberToDisplay);

        StartCoroutine(DecreaseTime());
    }

    IEnumerator DecreaseTime()
    {
        while (CurrentTime > 0)
        {
            yield return new WaitForSeconds(4f);
            CurrentTime--;
            RenderTextImage(CurrentTime.ToString());
        }
    }

    public void RenderTextImage(string text)
    {

        foreach (Transform child in parentContainer)
        {
            Destroy(child.gameObject);
        }

        // ���ڿ��� �� ���ھ� ó��
        foreach (char c in text.ToUpper()) 
        {
            if (c == ' ') // ���� ó��
            {
                CreateSpace();
            }

            else if (c >= '0' && c <= '9') // ���� ó��
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
