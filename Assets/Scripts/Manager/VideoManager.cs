using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage cutsceneRawImage;
    private float originalBGMVolume = 1f;
    private AsyncOperation loadSceneOp;

    public static VideoManager Instance { get; private set; }

    private bool isPlayingCutscene = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            videoPlayer.SetTargetAudioSource(0, audioSource);
        
        videoPlayer.EnableAudioTrack(0, true);
        
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.loopPointReached += OnVideoEnd;
        
        if (cutsceneRawImage != null)
            cutsceneRawImage.enabled = false;
    }
    
    public void PlayCutscene()
    {
        if (isPlayingCutscene) return;

        isPlayingCutscene = true;
        videoPlayer.Stop();
        
        if (AudioManager.Instance != null && AudioManager.Instance.bgmSource != null)
        {
            originalBGMVolume = AudioManager.Instance.bgmSource.volume;
            AudioManager.Instance.bgmSource.volume = 0f;
        }

        if (!videoPlayer.isPrepared)
        {
            videoPlayer.Prepare();
        }
        else
        {
            StartPlayback();
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        StartPlayback();
    }

    private void StartPlayback()
    {
        if (cutsceneRawImage != null)
            cutsceneRawImage.enabled = true;
        
        loadSceneOp = SceneManager.LoadSceneAsync("SecondStage");
        loadSceneOp.allowSceneActivation = false;

        videoPlayer.Play();
        
        StartCoroutine(CheckVideoEnd());
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (cutsceneRawImage != null)
            cutsceneRawImage.enabled = false;

        SceneManager.LoadScene("SecondStage");
    }

    private IEnumerator CheckVideoEnd()
    {
        while (videoPlayer.isPlaying)
        {
            if (videoPlayer.length - videoPlayer.time <= 0.5f)
            {
                if (AudioManager.Instance != null && AudioManager.Instance.bgmSource != null)
                {
                    AudioManager.Instance.bgmSource.volume = originalBGMVolume;
                    AudioManager.Instance.bgmSource.mute = false;
                }
                
                if (cutsceneRawImage != null)
                    cutsceneRawImage.enabled = false;
                
                loadSceneOp.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }
    }
}