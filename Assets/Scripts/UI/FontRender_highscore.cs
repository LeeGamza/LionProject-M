using System;
using UnityEngine;
using UnityEngine.UI;

public class FontRender_highscore : MonoBehaviour
{
    public string textToDisplay; 
    public Transform parentContainer; 
    public Sprite[] letterSprites; // "High_Score_0" ~ "High_Score_25" (A-Z)
    public Sprite[] numberSprites; // "High_Score_26" ~ "High_Score_36" (0-10)
    public Sprite[] symbolSprites; // 
    public float spaceWidth = 50f;
    public float fontSizeX = 50f;
    public float fontSizeY = 50f;

    void Start()
    {
        RenderTextImage(textToDisplay);
    }

    public void SetText(string text)
    {
        RenderTextImage(text);
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
            else if (c >= 'A' && c <= 'Z') // 알파벳 처리
            {
                int index = c - 'A';
                CreateLetterImage(letterSprites, index, c.ToString());
            }
            else if (c >= '0' && c <= '9') // 숫자 처리
            {
                int index = (c - '0') + 26;
                CreateLetterImage(numberSprites, index - 26, c.ToString());
            }
            else if (c == '!')
            {
                CreateLetterImage(symbolSprites, 0, c.ToString());
            }
            else if (c == '?')
            {
                CreateLetterImage(symbolSprites, 1, c.ToString());
            }
            else if (c == '.')
            {
                CreateLetterImage(symbolSprites, 2, c.ToString());
            }
            else if (c == '@')
            {
                CreateLetterImage(symbolSprites, 3, c.ToString());
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
            rect.sizeDelta = new Vector2(fontSizeX, fontSizeY); 
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
