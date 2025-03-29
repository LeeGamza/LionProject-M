using UnityEngine;

public class GameOver_dead : MonoBehaviour
{
    public CanvasGroup ContinueUI;
    private GameObject player;
    private bool isplayerlive = true;


    void Update()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            ContinueUI.alpha = 0;
        }
        else
        {
            ContinueUI.alpha = 1;
            isplayerlive = false;
        }
    }

    public bool Getisplayerlive()
    {
        return isplayerlive;
    }

}
