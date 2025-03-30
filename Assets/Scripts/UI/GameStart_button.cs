using UnityEngine;
using UnityEngine.UI;

public class GameStart_button : MonoBehaviour
{

    public Button startButton;

    void Start()
    {
        startButton.onClick.RemoveAllListeners();

        startButton.onClick.AddListener(() => EventManager.Instance.InvokeStartGame());
        startButton.onClick.AddListener(() => AudioManager.Instance.PlaySFX(AudioManager.Instance.insertCoin));
    }


}
