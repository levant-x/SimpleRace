using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopStopPage : MonoBehaviour
{
    public InputField nameInput;


    public void Submit()
    {
        if (nameInput.text.Trim() == string.Empty) return;
        gameObject.SetActive(false);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
