using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }



    // Update is called once per frame

    void Update()

    {

        MoveEnemy();

    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }


    private void MoveEnemy()

    {

        var targetPos = waypoints[waypointIndex].transform.position;

        var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

        transform.position = Vector2.MoveTowards

          (transform.position, targetPos, movementThisFrame);

        if (transform.position == targetPos)

        {

            waypointIndex += 1;

        }

        if (waypointIndex == waypoints.Count)

        {

            waypointIndex = 0;
            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
            if (enemySpawner.GetDestroy())
            {
              
                Destroy(gameObject);
            }
        }
    }
}
