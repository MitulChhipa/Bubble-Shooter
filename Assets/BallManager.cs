using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BallManager : MonoBehaviour
{
    [SerializeField] private BallScriptable[] ballScriptable;                          //All colored ball data
    [SerializeField] private GameObject Ball;                                          //destroyable ball prefab
    private GameObject[,] balls;                                                       //destroyable balls pool
    private float OffSet = 1f;                                                         //distance between balls with repect to centers
    [SerializeField] private GameObject _announcementPanel;
    [SerializeField] private TextMeshProUGUI _announcementText;


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
        _announcementPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ClearUnattachedBalls();
        CheckAllBalls();
    }

    //Instantiation destroyable balls matrix
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
                if (i >= 5)
                {
                    balls[i, j].SetActive(false) ;
                }
            }
        }
    }

    //Creating ball if the shooting ball and destroyable ball have different color
    //Here "ball" is the scriptable object from which color is determined and "col" is the column value for creating the ball in that column
    public void CreateNewBall(BallScriptable ball, int col)
    {
        bool foundSpace = false;
        for(int i=0; i < 10; i++)
        {
            if (!balls[i, col].activeInHierarchy)
            {
                foundSpace = true;
                balls[i, col].SetActive(true);
                balls[i, col].GetComponent<GridBallBehavior>().ballValue = ball;
                balls[i, col].GetComponent<GridBallBehavior>().UpdateMaterial(transform.position + new Vector3(col * OffSet, 0.5f, -i * OffSet));
                return;
            }
        }
        if (!foundSpace)
        {
            Lost();
        }
    }

    //destroying balls that are not attached to the ceiling
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

    //Checking all balls in the pool if active or not
    //If there is no active ball then calls win funtion
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

    //After win funtion
    private void Win()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        _announcementPanel.SetActive(true);
        _announcementText.text = "You Won";
    }

    private void Lost()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined; 
        _announcementPanel.SetActive(true);
        _announcementText.text = "You Lost";
    }

    //Resetting the colors of the balls and activating them
    public void ResetAllBalls()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                balls[i, j].SetActive(true);
                balls[i,j].GetComponent<Rigidbody>().isKinematic = true;
                GridBallBehavior y = balls[i, j].GetComponent<GridBallBehavior>();
                y.ballValue = ballScriptable[Random.Range(0, ballScriptable.Length)];
                y.UpdateMaterial(transform.position + new Vector3(j * OffSet, 0.5f, -i * OffSet));
                if (i >= 5)
                {
                    balls[i, j].SetActive(false);
                }
            }
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
}
