using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private BallScriptable[] ballScriptables;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Rigidbody rb;
    public BallScriptable currentScriptable;
    [SerializeField] private ShootingBehavior Shooter;


    private void Start()
    {
        ChangeScriptable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (currentScriptable.colorType == collision.gameObject.GetComponent<GridBallBehavior>().ballValue.colorType)
            {
                rb.isKinematic = true;
                gameObject.SetActive(false);
                Invoke("ResetValues", 0.5f);
                return; 
            }
            else
            {
                rb.isKinematic = true;
                gameObject.SetActive(false);
                Invoke("ResetValues", 0.5f);
                BallManager.instance.CreateNewBall(currentScriptable, collision.gameObject.GetComponent<GridBallBehavior>().col);
            }
        }
    }

    //Reseting the shooting ball back to its shooting position
    private void ResetValues()
    {

        Shooter.ResetBall();
        ChangeScriptable();
    }

    //Changing the color of the shooting ball
    private void ChangeScriptable()
    {
        currentScriptable = ballScriptables[Random.Range(0,ballScriptables.Length)];
        meshRenderer.material = currentScriptable.material;
    }
}
