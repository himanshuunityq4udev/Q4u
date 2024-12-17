using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sFXSource;

    [SerializeField] private AudioClip background;
    [SerializeField] private AudioClip coinHit;
    [SerializeField] private AudioClip lifeHit;
    [SerializeField] private AudioClip levelComplete;
    [SerializeField] private AudioClip levelFailed;
    [SerializeField] private AudioClip coinBuyOrSpend;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip rolling;



    private void Start()
    {
        _musicSource.clip = background;
        _musicSource.Play();
    }


    public void playSfX(AudioClip _clip)
    {
        _sFXSource.clip = _clip;
    }

}
