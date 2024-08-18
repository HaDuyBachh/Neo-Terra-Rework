using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoadScene : MonoBehaviour
{
    // [FormerlySerializedAs("SceneToLoad")] public string sceneToLoad;
    // [FormerlySerializedAs("SceneToHide")] public string sceneToHide;
    public int sceneIndex;
    public int sceneIndextoHide;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ok");
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(sceneIndextoHide);

    }
}