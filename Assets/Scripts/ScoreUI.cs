using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI poissonText;
    // Start is called before the first frame update
    void Start()
    {
        poissonText = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void UpdatePoissonText(PlayerInvention playerInvention)

    {
        poissonText.text = playerInvention.NumberOfPoissons.ToString();
        
    }
}
