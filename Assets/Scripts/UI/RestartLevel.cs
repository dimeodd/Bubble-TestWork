using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class RestartLevel : MonoBehaviour
{
    void Start()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(RestarLevel);
    }

    // Update is called once per frame
    void RestarLevel()
    {
        SceneManager.LoadScene("World");
    }
}
