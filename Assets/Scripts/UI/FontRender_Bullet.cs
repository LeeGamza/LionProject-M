using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FontRender_Bullet : MonoBehaviour
{
    public Transform parentContainer; 
    public Sprite[] numberSprites; 
    public float spaceWidth = 50f;
    public float fontSizeX = 50f;
    public float fontSizeY = 50f;
    public bool isARMS = true;

    private FireManager fireManager;

    private int previousAmmo = -1; 
    private int previousMissile = -1; 

    void Start()
    {

        if (isARMS)
        {
            //int currentAmmo = fireManager.Curruntammo();
            int currentAmmo = 200; // 임시 출력 
            previousAmmo = currentAmmo; 
            RenderNumberImage(currentAmmo);
        }

        else
        {
            int currentMissile = 10;  // 이 부분을 받아와서 적용
            previousMissile = currentMissile; 
            RenderNumberImage(currentMissile);
        }
    }

    void Update()
    {
        int currentAmmo = fireManager.Curruntammo();
        int currentMissile = 10; 

        if (currentAmmo != previousAmmo) 
        {
            RenderNumberImage(currentAmmo);
            previousAmmo = currentAmmo; 
        }

        if (currentMissile != previousMissile)
        {
            RenderNumberImage(currentMissile);
            previousAmmo = currentMissile;
        }
    }


    public void RenderNumberImage(int number)
    {
        
        foreach (Transform child in parentContainer)
        {
            Destroy(child.gameObject);
        }

       
        string text = number.ToString();

        foreach (char c in text)
        {
            if (c == ' ') 
            {
                CreateSpace();
            }
            if (c >= '0' && c <= '9')
            {
                int index = c - '0'; 
                CreateLetterImage(numberSprites, index, c.ToString());
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
