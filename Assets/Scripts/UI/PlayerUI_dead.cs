using UnityEngine;

public class PlayerUI_dead : MonoBehaviour
{
    public CanvasGroup PlayerUI;
    private GameObject player; 


    void Start()
    {

    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            PlayerUI.alpha = 1;
        }
        else
        {
            PlayerUI.alpha = 0;
        }
    }
}
