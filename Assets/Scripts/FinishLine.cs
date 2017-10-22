using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour {
	public int LevelIndex;
    Scene sceneLoaded;
	// Use this for initialization
	void Start () {
        //Scene sceneLoaded = SceneManager.GetActiveScene();
        // loads next level
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
			SceneManager.LoadScene(LevelIndex);
            //SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
        }
    }
}
