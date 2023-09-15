using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallManager : MonoBehaviour
{
    [SerializeField] private BallScriptable[] ballScriptable;
    [SerializeField] private GameObject Ball;
    private GameObject[,] balls;
    private float OffSet = 1f;
    public static BallManager instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        balls = new GameObject[10, 10];
        print(balls.Length);
        InstantiateBalls();
    }

    private void Update()
    {
        ClearUnattachedBalls();
        CheckAllBalls();
    }

    private void InstantiateBalls()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                balls[i, j] = Instantiate(Ball, transform.position + new Vector3(j * OffSet, 0.5f, -i * OffSet), Quaternion.identity, transform);
                GridBallBehavior y = balls[i, j].GetComponent<GridBallBehavior>();
                y.ballValue = ballScriptable[Random.Range(0, ballScriptable.Length)];
                y.row = i;
                y.col = j;
                if (i >= 4)
                {
                    balls[i, j].SetActive(false) ;
                }
            }
        }
    }

    public void CreateNewBall(BallScriptable ball, int col)
    {
        for(int i=0; i < 10; i++)
        {
            if (!balls[i, col].activeInHierarchy)
            {
                balls[i, col].SetActive(true);
                balls[i, col].GetComponent<GridBallBehavior>().ballValue = ball;
                balls[i, col].GetComponent<GridBallBehavior>().UpdateMaterial(transform.position + new Vector3(col * OffSet, 0.5f, -i * OffSet));
                return;
            }
        }
    }

    public void ClearUnattachedBalls()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (!balls[i, j].activeInHierarchy)
                {
                    for (int k = i; k < 10; k++)
                    {
                        balls[k, j].SetActive(false);
                    }
                }
            }
        }
    }
    public void CheckAllBalls()
    {
        for(int i=0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                if(balls[i, j].activeInHierarchy)
                {
                    return;
                }
            }
        }
        Win();
    }
    private void Win()
    {
        print("Win");
        Time.timeScale = 0f;
    }
}
