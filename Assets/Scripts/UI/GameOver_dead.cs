using UnityEngine;

public class GameOver_dead : MonoBehaviour
{
    public CanvasGroup ContinueUI;
    public GameObject Count;
    private GameObject player;
    private bool isplayerlive = true;
    private bool isCount = false;


    void Update()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {

            ContinueUI.alpha = 0;
            isplayerlive = true;
   
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
