using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BolumArasi : MonoBehaviour


{
    [SerializeField] private Button nextButton;
    [SerializeField] private int sceneIndex;

    private void Awake()
    {
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(() => {
                SceneManager.LoadScene(sceneIndex);
            });
        }
        else
        {
            Debug.LogError("Next button is not assigned in the inspector!");
        }
    }



}
