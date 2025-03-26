using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_SlugHp : MonoBehaviour
{
    public Transform parentContainer;
    public Sprite[] HpSprites; 

    public int SlugHp;        
    public int MaxHp = 100;    

    public float fontSize = 50f;

    private const int totalSlots = 7;  // HP 바 내부 칸 개수
    private const int spriteSteps = 9; // 한 칸당 9개의 스프라이트
    private const int totalSteps = totalSlots * spriteSteps; 

    void Start()
    {   
        UpdateHPBar(SlugHp, MaxHp);
    }

 
 

    public void UpdateHPBar(int currentHP, int maxHP)
    {
        
        foreach (Transform child in parentContainer)
        {
            Destroy(child.gameObject);
        }

        if (maxHP <= 0) return; 

        int filledSteps = Mathf.RoundToInt((currentHP / (float)maxHP) * totalSteps); // 현재 HP 비율 계산

       
        CreateLetterImage(HpSprites, 0, "HP_Left");


        for (int i = 0; i < totalSlots; i++)
        {
            int step = Mathf.Clamp(filledSteps - (i * spriteSteps), 1, 9); 
            CreateLetterImage(HpSprites, step, "HP_Sprite_" + i);
        }

       
        CreateLetterImage(HpSprites, 10, "HP_Right");
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

            float width = fontSize;
            float height = fontSize;

            if (index == 0 || index == 10) 
            {
                width *= 0.25f;
            }

            rect.sizeDelta = new Vector2(width, height);
        }
    }




}