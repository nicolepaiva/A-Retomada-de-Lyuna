using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlaylist : MonoBehaviour
{
    public VideoPlayer videoPlayer;   // arraste o componente VideoPlayer aqui
    public string sceneToLoad;        // nome da cena final

    void Start()
    {
        // quando o vídeo terminar, chama OnVideoFinished
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
