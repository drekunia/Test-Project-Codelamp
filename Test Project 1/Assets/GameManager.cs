using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int totalPicked = 0;
    public int requiredPick = 10;

    [SerializeField] TMP_Text textProgress;
    [SerializeField] GameObject finishUI;
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip clipPicking;
    [SerializeField] AudioClip clipFinish;

    public void AddPickedObject()
    {
        totalPicked+=1;
        textProgress.text = totalPicked.ToString() + "/" + requiredPick.ToString();
        audioSource.clip = clipPicking;
        audioSource.Play();
    }

    public void Finish()
    {
        if(totalPicked >= requiredPick)
        {
            finishUI.SetActive(true);
            audioSource.clip = clipFinish;
            audioSource.Play();
        }
    }
}
