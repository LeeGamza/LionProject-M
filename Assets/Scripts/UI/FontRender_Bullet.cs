using System;
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

    private int currentAmmo;
    private int currentMissile;

    private int previousAmmo; 
    private int previousMissile; 

    void Start()
    {
        Player player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("Player not Found!");
            return;
        }
        if (player != null)
        {
            fireManager = player.GetFireManager();
        }

        currentAmmo = fireManager.Curruntammo();
        // currentMissile = fireManager.Curruntmissile();
        currentMissile = 10;

        if (isARMS)
        {
            RenderInfinityImage();         
        }
        else
        {
            previousMissile = currentMissile; 
            RenderNumberImage(currentMissile);
        }
    }
    void Update()
    {
        if (fireManager == null) return;

        if (isARMS)
        {
            currentAmmo = fireManager.Curruntammo();
            if (currentAmmo != previousAmmo)
            {
                if (currentAmmo > 0)
                {
                    RenderNumberImage(currentAmmo);
                    previousAmmo = currentAmmo;
                }
                else
                {
                    RenderInfinityImage();
                    previousAmmo = currentAmmo;
                }
            }
        }
        else
        {
            //currentMissile = fireManager.Curruntmissile();
            if (currentMissile != previousMissile)
            {
                RenderNumberImage(currentMissile);
                previousMissile = currentMissile;
            }
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

    public void RenderInfinityImage()
    {
        foreach (Transform child in parentContainer)
        {
            Destroy(child.gameObject);
        }


        GameObject letterObj = new GameObject("Infinity", typeof(Image));
        letterObj.transform.SetParent(parentContainer, false);

        Image img = letterObj.GetComponent<Image>();
        img.sprite = numberSprites[10];

        RectTransform rect = letterObj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(35, 17);
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
