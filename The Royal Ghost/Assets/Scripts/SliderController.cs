using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        slider.value = MusicManager.Instance.GetComponent<AudioSource>().volume;
    }

    private void OnSliderValueChanged(float value)
    {
        MusicManager.Instance.SetVolume(value);
    }
}
