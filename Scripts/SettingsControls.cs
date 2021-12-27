using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsControls : MonoBehaviour
{
    bool isFullScreen = true;
    public AudioMixer am;
    Resolution[] resArr;
    List<string> resList = new List<string>();
    public Dropdown dropdown;
    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void SetVolume(float volume) 
    {
        am.SetFloat("masterVolume", volume);
    }

    public void SetNickName(string nick)
    {
        InterSceneBuffer.NickName = nick;
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(resArr[index].width, resArr[index].height, isFullScreen);
    }

    public void Awake()
    {
        InterSceneBuffer.AM = am;
        resArr = Screen.resolutions;
        foreach (var i in resArr)
        {
            resList.Add(i.width + "x" + i.height);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(resList);
        dropdown.value = dropdown.options.Count - 1;
    }
}
