using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartTrigger : MonoBehaviour
{
    [SerializeField] private Transform checpoint;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Restart");
        //SceneManager.LoadScene(0);
        collision.gameObject.transform.position = checpoint.transform.position;
    }
}
