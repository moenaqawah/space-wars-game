using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //singleton pattren to the music
        var musicPlayersSize = FindObjectsOfType<MusicPlayer>().Length;
        if (musicPlayersSize > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        
    }

  
}
