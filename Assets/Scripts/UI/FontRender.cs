using UnityEngine;
using UnityEngine.UI;

public class ImageFontRenderer : MonoBehaviour
{
    public string textToDisplay; 
    public Transform parentContainer; 
    public Sprite[] letterSprites; // "High_Score_0" ~ "High_Score_25" (A-Z)
    public Sprite[] numberSprites; // "High_Score_26" ~ "High_Score_36" (0-10)
    public float spaceWidth = 50f;
    public float fontSize = 50f;

    void Start()
    {
        RenderTextImage(textToDisplay);
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
            else if (c >= 'A' && c <= 'Z') // ���ĺ� ó��
            {
                int index = c - 'A'; 
                CreateLetterImage(letterSprites, index, c.ToString());
            }
            else if (c >= '0' && c <= '9') // ���� ó��
            {
                int index = (c - '0') + 26;
                CreateLetterImage(numberSprites, index - 26, c.ToString());
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
