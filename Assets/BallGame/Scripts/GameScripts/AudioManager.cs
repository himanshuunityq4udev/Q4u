using UnityEngine;
using CandyCoded.HapticFeedback;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [Header("Audio SourcesS")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sFXSource;

    [Header("Audio clips")]
    [SerializeField] private AudioClip background;
    [SerializeField] private AudioClip coinHit;
    [SerializeField] private AudioClip lifeHit;
    [SerializeField] private AudioClip levelComplete;
    [SerializeField] private AudioClip levelFailed;
    [SerializeField] private AudioClip coinBuyOrSpend;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip rolling;


    [SerializeField] Button hapticButtonOn;
    [SerializeField] Button hapticButtonOff;

    [SerializeField] Button musicButtonOn;
    [SerializeField] Button musicButtonOff;

    [SerializeField] Button sfxButtonOn;
    [SerializeField] Button sfxButtonOff;


    private const string HapticToggleKey = "HapticToggle";
    private const string MusicToggleKey = "MusicToggle";
    private const string SFXToggleKey = "SFXToggle";

    private bool tempHapticState; // Temporary state to track toggling without saving
    private bool tempMusicState; // Temporary state to track toggling without saving
    private bool tempSFXState; // Temporary state to track toggling without saving


    private void Awake()
    {
        // Initialize PlayerPrefs if not already set
        if (!PlayerPrefs.HasKey(HapticToggleKey))
        {
            SetPlayHaptic(true); // Default value is true

        }
        if (!PlayerPrefs.HasKey(MusicToggleKey))
        {
            SetPlayHaptic(true); // Default value is true

        }
        if (!PlayerPrefs.HasKey(SFXToggleKey))
        {
            SetPlayHaptic(true); // Default value is true

        }
    }

    private void Start()
    {
        // Load saved haptic state
        tempHapticState = GetPlayHaptic();
        tempMusicState = GetPlayMusic();
        tempSFXState = GetPlaySFX();

        // Set button states based on current haptic settings
        UpdateButtonStates();
        PlayMusic();
    }

    private void OnEnable()
    {
        ActionHelper.PlayHaptic += PlayHaptic;
    }
    private void OnDisable()
    {
        ActionHelper.PlayHaptic -= PlayHaptic;
    }

    public void EnableHaptics()
    {
        tempHapticState = true; // Update temporary state
        UpdateButtonStates();
    }

    public void DisableHaptics()
    {
        tempHapticState = false; // Update temporary state
        UpdateButtonStates();
    }

    public void EnableMusic()
    {
        tempMusicState = true; // Update temporary state
        UpdateButtonStates();
    }

    public void DisableMusic()
    {
        tempMusicState = false; // Update temporary state
        UpdateButtonStates();
    }

    public void EnableSFX()
    {
        tempSFXState = true; // Update temporary state
        UpdateButtonStates();
    }

    public void DisableSFX()
    {
        tempSFXState = false; // Update temporary state
        UpdateButtonStates();
    }


    public void SaveSettings()
    {
        // Save temporary state to PlayerPrefs
        SetPlayHaptic(tempHapticState);
        SetPlayMusic(tempMusicState);
        SetPlaySFX(tempSFXState);
    }

    public void CloseSettings()
    {
        // Revert to the last saved state
        tempHapticState = GetPlayHaptic();
        tempMusicState = GetPlayMusic();
        tempSFXState = GetPlaySFX();
        UpdateButtonStates();
    }


    public void UpdateButtonStates()
    {
      // bool isHapticEnabled = GetPlayHaptic();

        if (hapticButtonOn != null) hapticButtonOn.gameObject.SetActive(!tempHapticState);
        if (hapticButtonOff != null) hapticButtonOff.gameObject.SetActive(tempHapticState);

        if (musicButtonOn != null) musicButtonOn.gameObject.SetActive(!tempMusicState);
        if (musicButtonOff != null) musicButtonOff.gameObject.SetActive(tempMusicState);

        if (sfxButtonOn != null) sfxButtonOn.gameObject.SetActive(!tempSFXState);
        if (sfxButtonOff != null) sfxButtonOff.gameObject.SetActive(tempSFXState);

    }


    private void SetPlayHaptic(bool isEnabled)
    {
        PlayerPrefs.SetInt(HapticToggleKey, isEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    private bool GetPlayHaptic()
    {
        return PlayerPrefs.GetInt(HapticToggleKey, 1) == 1; // Default to true if key not found
    }


    private void SetPlayMusic(bool isEnabled)
    {
        PlayerPrefs.SetInt(MusicToggleKey, isEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    private bool GetPlayMusic()
    {
        return PlayerPrefs.GetInt(MusicToggleKey, 1) == 1; // Default to true if key not found
    }


    private void SetPlaySFX(bool isEnabled)
    {
        PlayerPrefs.SetInt(SFXToggleKey, isEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    private bool GetPlaySFX()
    {
        return PlayerPrefs.GetInt(SFXToggleKey, 1) == 1; // Default to true if key not found
    }



    private void PlayHaptic()
    {
        if (GetPlayHaptic())
        {
            HapticFeedback.MediumFeedback();
        }
    }

    private void PlayMusic()
    {
        if (GetPlayMusic())
        {
            _musicSource.clip = background;
            _musicSource.Play();
        }
    }



    public void playSfX(AudioClip _clip)
    {
        if (GetPlaySFX())
        {
            _sFXSource.clip = _clip;
        }
    }

}
