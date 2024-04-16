using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]

    public AudioClip background;
    public AudioClip death;
    public AudioClip checkPoint;
    public AudioClip damageTaken;
    public AudioClip attack;
    public AudioClip jump;
    public AudioClip Heal;
    public AudioClip collectibles;
    public AudioClip SelectBtn;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void SFX_Play(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }

}
