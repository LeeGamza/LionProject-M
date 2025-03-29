using UnityEngine;

public class UI_Player_Score : MonoBehaviour
{
    public FontRender_highscore highscore;
    public float currentScore;
    public float previousScore;


    void Start()
    {
        highscore = GetComponent<FontRender_highscore>();
    }

    void Update()
    {
        if(highscore !=null && currentScore != previousScore)
        {
            previousScore = currentScore;
            highscore.SetText(((int)currentScore).ToString());
        }
    }


}
