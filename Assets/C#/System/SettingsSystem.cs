using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SettingsSystem : MonoBehaviour
{
    public AudioSource MusicSource;

    public AudioSource SoundSource;

    public Slider MusicSlider;

    public Slider SoundSlider;

    public Text MusicText;

    public Text SoundText;

    private void Awake()
    {
        YandexGame.LoadProgress();

        YandexGame.SwitchLanguage(YandexGame.savesData.language);

        YandexGame.lang = YandexGame.savesData.language;
    }

    private void Start()
    {
        YandexGame.LoadProgress();

        MusicSlider.value = YandexGame.savesData.MusicSliderValue;

        MusicSource.volume = MusicSlider.value / 200f;

        SoundSlider.value = YandexGame.savesData.SoundSliderValue;

        SoundSource.volume = SoundSlider.value / 200f;
    }

    public void ChangeMusic()
    {
        MusicSource.volume = MusicSlider.value / 200f;

        MusicText.text = $"{MusicSlider.value} / 100";

        YandexGame.savesData.MusicSliderValue = MusicSlider.value;

        YandexGame.SaveProgress();
    }

    public void ChangeSound()
    {
        SoundSource.volume = SoundSlider.value / 200f;

        SoundText.text = $"{SoundSlider.value} / 100";

        YandexGame.savesData.SoundSliderValue = SoundSlider.value;

        YandexGame.SaveProgress();
    }

    public void ChangeLanguage(string language)
    {
        YandexGame.SwitchLanguage(language);

        YandexGame.lang = language;

        YandexGame.savesData.language = language;

        YandexGame.SaveProgress();
    }
}
