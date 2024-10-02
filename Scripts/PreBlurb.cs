using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreBlurb : MonoBehaviour
{
    public RawImage rawImage;
    public Button xButton;

    // Start is called before the first frame update
    void Start()
    {
        ShowObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowObject()
    {
        rawImage.enabled = true;
        xButton.interactable = true;
    }

    public void HideObject()
    {
        rawImage.enabled = false;

        Image buttonImage = xButton.GetComponent<Image>();

        // Disable the Image component to hide the image
        buttonImage.enabled = false;
    }
}
