using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlaylist : MonoBehaviour
{
    public AudioSource audioSource;
    public VideoPlayer videoPlayer;
    public string[] videoFiles; // Lista de vídeos (coloque os caminhos ou URLs)
    public string sceneToLoad;  // Cena final
    private int currentIndex = 0;

    void Start()
    {
        // Configura saída de áudio
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.loopPointReached += OnVideoFinished;
        PlayVideo(currentIndex);

        // Evento quando o vídeo termina
       // videoPlayer.loopPointReached += OnVideoFinished;
    }

    void PlayVideo(int index)
    {
        if (index < videoFiles.Length)
        {
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFiles[index]);
            videoPlayer.Play();
        }
        else
        {
            // Se acabou a lista, carrega a cena
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        currentIndex++;
        PlayVideo(currentIndex);
    }
}
