using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Button))]
public class MuteButton : MonoBehaviour
{
    public Image Image;
    public StaticData StaticData;
    public AudioSource Audio;

    void Start()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(SwitchSoundSound);

        var isMute = PlayerPrefs.GetInt("Mute", 0) == 1;
        if (isMute)
            TurnOff();
        else
            TurnOn();
    }

    void SwitchSoundSound()
    {
        var isMute = PlayerPrefs.GetInt("Mute", 0) == 1;
        isMute = !isMute;

        if (isMute)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
            Audio.Play();
        }

        int IntIsMute = isMute ? 1 : 0;
        PlayerPrefs.SetInt("Mute", IntIsMute);
        PlayerPrefs.Save();
    }

    void TurnOff()
    {
        StaticData.AudioMixer.SetFloat("masterVol", -80);
        Image.sprite = StaticData.SoundOffSprite;
    }

    void TurnOn()
    {
        StaticData.AudioMixer.SetFloat("masterVol", 0);
        Image.sprite = StaticData.SoundOnSprite;
    }
}
