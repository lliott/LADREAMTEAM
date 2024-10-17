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
    }

    void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
