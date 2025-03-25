using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }
    private bool needAspectFix = false;
    private string targetScene = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        targetScene = scene.name;
        ApplyAspectIfCameraExists();
    }

    private void Update()
    {
        if (needAspectFix)
        {
            ApplyAspectIfCameraExists();
        }
    }

    private void ApplyAspectIfCameraExists()
    {
        if (Camera.main == null)
        {
            needAspectFix = true;
            return;
        }

        needAspectFix = false;

#if !UNITY_EDITOR
        if (targetScene == "SecondStage")
        {
            Screen.SetResolution(720, 960, false); // 3:4 비율
        }
        else
        {
            Screen.SetResolution(1280, 720, false); // 16:9
        }
#endif

        if (targetScene == "SecondStage")
        {
            Camera.main.aspect = 3f / 4f; // 720:960 = 3:4
            Debug.Log("[SceneManager] SecondStage: 3:4 비율 설정됨");
        }
        else
        {
            Camera.main.aspect = 16f / 9f;
            Debug.Log("[SceneManager] Default: 16:9 비율 설정됨");
        }
    }
}