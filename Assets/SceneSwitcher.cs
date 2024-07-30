using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(1);
    }
}
