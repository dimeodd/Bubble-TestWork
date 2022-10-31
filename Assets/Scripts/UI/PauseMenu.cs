using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isPause;
    public Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(TurnOff);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }
    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        isPause = true;
    }

    private void OnDisable()
    {
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if (Input.backButtonLeavesApp)
        {
            gameObject.SetActive(false);
            isPause = false;
        }
#endif
    }
}
