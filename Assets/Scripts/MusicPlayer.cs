using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource, loopSource;

    private bool wasPaused = false;

    void Start()
    {
        introSource.Play();
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);
    }

    void Update()
    {
        if (PauseMenu.GameIsPaused && !wasPaused)
        {
            introSource.Pause();
            loopSource.Pause();
            wasPaused = true;
        }
        else if (!PauseMenu.GameIsPaused && wasPaused)
        {
            introSource.UnPause();
            loopSource.UnPause();
            wasPaused = false;
        }
    }
}
