using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isPause;
    public Button closeButton;
    public float timeFromPause;
    public GameObject PausePanel;

    private void Awake()
    {
        closeButton.onClick.AddListener(TurnOff);
    }

    public void TurnOn()
    {
        PausePanel.SetActive(true);
        isPause = true;
    }
    public void TurnOff()
    {
        PausePanel.SetActive(false);
        isPause = false;
        timeFromPause = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeFromPause < 10)
            timeFromPause += Time.deltaTime;

#if UNITY_ANDROID
        if (Input.backButtonLeavesApp)
        {
            gameObject.SetActive(false);
            isPause = false;
        }
#endif
    }
}
