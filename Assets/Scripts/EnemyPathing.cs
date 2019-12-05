using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    
    
    WaveConfig waveConfig;


    List<Transform> waypoints = new List<Transform>();
    int waypointsIndex=0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.getPathWayPoints();
        transform.position = waypoints[waypointsIndex].transform.position;
        waypointsIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }

    public void setWaveConfigForEnemy(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void moveEnemy()
    {
       
        if (waypointsIndex <= waypoints.Count-1)
        {
            var movementThisFrame = waveConfig.getMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointsIndex].transform.position, movementThisFrame);
          
            if ((Vector2) transform.position == (Vector2) waypoints[waypointsIndex].position)
            {
                waypointsIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
