using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Collections;

public class RetryButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
    }
}
