using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sensslider : MonoBehaviour
{
    // slider that changes the camera sensitivity
    // TODO: combine both sliders into a singular script for ease of maintenance?
    // TODO: wrt VR should this even be a slider?

    // define all the variables and objects to steal from
    public float sensitivity = 0.0f; // camera sensitivity value
    [SerializeField] private Slider _slider; // this basically allows unity to recognise that the slider exists and is a slider
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); }); // add a process that is alerted whenever the slider's value is changed and sees how to process that
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChangeCheck() // function for actually handling value changes
    {
        sensitivity = _slider.value; // set  camera sensitivity to the slider's value
    }
}