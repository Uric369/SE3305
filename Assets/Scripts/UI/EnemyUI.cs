using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyUI : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Image> dragonBloodImage;
    public float percentage;

    public void bloodUpdate(float percentage, int index)
    {
        this.percentage = percentage;
        dragonBloodImage[index].fillAmount = percentage;
    }
}



