using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Image bloodImage;
    public Image energyImage;

    public void bloodUpdate(float percentage)
    {
        bloodImage.fillAmount = percentage;
    }

   
}
