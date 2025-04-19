using UnityEngine;

public class PlayVinylAudio : MonoBehaviour
{
    public AudioClip[] tracks; 
    private AudioSource audioSource;
    private int currentTrackIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f; 
        audioSource.loop = false; 
        audioSource.playOnAwake = false; 

        if (tracks.Length > 0)
        {
            PlayTrack(currentTrackIndex);
        }
    }

    void Update()
    {
        
        if (!audioSource.isPlaying)
        {
            NextTrack();
        }
    }

    void PlayTrack(int trackIndex)
    {
        if (tracks.Length == 0) return; 

        audioSource.clip = tracks[trackIndex];
        audioSource.Play();
    }

    void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % tracks.Length; 
        PlayTrack(currentTrackIndex);
    }

    
    public void PlayRandomTrack()
    {
        int newTrackIndex = Random.Range(0, tracks.Length);
        while (newTrackIndex == currentTrackIndex && tracks.Length > 1)
        {
            newTrackIndex = Random.Range(0, tracks.Length);
        }

        currentTrackIndex = newTrackIndex;
        PlayTrack(currentTrackIndex);
    }
}
