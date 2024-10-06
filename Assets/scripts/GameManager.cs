using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode jump { get; set; }
    public KeyCode dash { get; set; }

    void Start()
    {
        if (GM == null)
        {
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        jump = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        dash = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dashKey", "LeftShift"));
    }
}
