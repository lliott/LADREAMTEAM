using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonData
    {
        public Button button;
        public int sceneIndex;
        public string buttonText;
    }

    public List<ButtonData> buttons = new List<ButtonData>();

    // Audio
    private AudioSource _audio;
    private float delay = 0.25f; 
    private float timer = 0f; 
    private bool isLoading = false; 
    private int sceneToLoad; 

    void Start()
    {
        foreach (ButtonData buttonData in buttons)
        {
            // Set the button text
            TextMeshProUGUI tmpText = buttonData.button.GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.text = buttonData.buttonText;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found in button's children.");
            }

            // Add listener to the button
            int index = buttonData.sceneIndex; // Local copy to avoid closure issues
            buttonData.button.onClick.AddListener(() => LoadSceneByIndex(index));
        }

        if (TryGetComponent<AudioSource>(out AudioSource audio))
        {
            _audio = audio;
        }
    }

    //att 0.25sec avant de load la scene
   void Update()
    {
        if (isLoading)
        {
            timer += Time.deltaTime; 

            if (timer >= delay)
            {
                SceneManager.LoadScene(sceneToLoad); 
                isLoading = false; 
                timer = 0f; 
            }
        }
    }

    //pouvoir play le son avant de load une scene
    void LoadSceneByIndex(int index)
    {
        if (_audio != null)
        {
            _audio.Play(); 
        }

        sceneToLoad = index; 
        Time.timeScale = 1;
        isLoading = true; 
    }
}
