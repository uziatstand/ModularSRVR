using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sensslider : MonoBehaviour
{
    public float sensitivity = 0.0f;
    [SerializeField] private Slider _slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChangeCheck()
    {
        sensitivity = _slider.value;
    }
}