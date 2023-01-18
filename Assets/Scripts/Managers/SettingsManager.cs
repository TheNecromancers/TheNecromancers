using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsManager : MonoSingleton<SettingsManager>
{


    [field:SerializeField, Range(-1,1)] float volumeValue;
    float defaultVolumeValue =0;
    [field:SerializeField, Range(-1,1)]  float postExposureValue;
    float defaultPostExposureValue =0;
    [field:SerializeField, Range(-25,25)] float contrastValue;
    float defaultContrastValue =0;

    [field:SerializeField] Sprite audioActiveSprite;
    [field:SerializeField] Sprite audioInactiveSprite;


    private Slider VolumeSlider;
    private Slider BrightnessSlider;
    private Slider ContrastSlider;
    private Button MuteButton;
    private Button DefaultButton;
    private Controls controls;
    private Volume globalVolume;
    

    override public void Awake() 
    {
        LoadPrefs();

    }

    public void FindObjects()
    {
        globalVolume =FindObjectOfType<Volume>();

        DefaultButton = GameObject.FindGameObjectWithTag("DefaultButton").GetComponent<Button>();
        DefaultButton.onClick.AddListener(SetDefaults);

        MuteButton = GameObject.FindGameObjectWithTag("MuteButton").GetComponent<Button>();
        MuteButton.onClick.AddListener(MuteUnmuteAudio);

        VolumeSlider = GameObject.FindGameObjectWithTag("VolumeSlider").GetComponent<Slider>();

        VolumeSlider.onValueChanged.AddListener(delegate{ChangeVolume(VolumeSlider.value);});
        VolumeSlider.onValueChanged.AddListener(delegate{debugDelegate();});

        BrightnessSlider = GameObject.FindGameObjectWithTag("BrightnessSlider").GetComponent<Slider>();
        BrightnessSlider.onValueChanged.AddListener(ChangeBrightness);

        ContrastSlider = GameObject.FindGameObjectWithTag("ContrastSlider").GetComponent<Slider>();
        ContrastSlider.onValueChanged.AddListener(delegate{ChangeContrast(ContrastSlider.value);});
    }

    public void debugDelegate()
    {
        Debug.Log("Moved The slider");
    }
    public void ChangeVolume(float _value)
    {
        PlayerPrefs.SetFloat("VolumeValue",_value);
        volumeValue =_value;
        if(_value ==-1)
        {
            MuteButton.image.sprite =audioInactiveSprite;
        }
        else
        {
            MuteButton.image.sprite =audioActiveSprite;
        }

    }
    public void ChangeBrightness(float _value)
    {
        globalVolume.profile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.postExposure.value = _value;
        PlayerPrefs.SetFloat("PostExposureValue",_value);     
        
    }
    public void ChangeContrast(float _value)
    {
        globalVolume.profile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.contrast.value =_value;
        PlayerPrefs.SetFloat("ContrastValue",_value);
    }


    public void SetDefaults()
    {
        volumeValue =defaultVolumeValue;
        ChangeVolume(volumeValue);
        VolumeSlider.value=volumeValue;
        postExposureValue = defaultPostExposureValue;
        ChangeBrightness(postExposureValue);
        BrightnessSlider.value=postExposureValue;
        contrastValue =defaultContrastValue;
        ChangeContrast(contrastValue);
        ContrastSlider.value=contrastValue;
        

    }
    public void LoadPrefs()
    {
        volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        VolumeSlider.value=volumeValue;
        ChangeVolume(volumeValue);
        postExposureValue = PlayerPrefs.GetFloat("PostExposureValue");
        BrightnessSlider.value=postExposureValue;
        ChangeBrightness(postExposureValue);
        contrastValue = PlayerPrefs.GetFloat("ContrastValue");
        ContrastSlider.value=contrastValue;
        ChangeContrast(contrastValue);
    }

    public void MuteUnmuteAudio()
    {
        if(MuteButton.image.sprite == audioActiveSprite)
        {
            MuteButton.image.sprite =audioInactiveSprite;
            volumeValue = -1;
            VolumeSlider.value=-1;
        }
        else
        {
            MuteButton.image.sprite =audioActiveSprite;
            volumeValue = defaultVolumeValue;
            VolumeSlider.value= defaultVolumeValue;
        }
    }






    void Update()
    {
        
    }
}
