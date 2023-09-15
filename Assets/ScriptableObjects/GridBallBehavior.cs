using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBallBehavior : MonoBehaviour
{
    public BallScriptable ballValue;
    [SerializeField]private MeshRenderer meshRenderer;
    public Rigidbody body;
    public int row;
    public int col;

    private void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = ballValue.material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ShootingBall"))
        {
            if(collision.gameObject.GetComponent<BallBehavior>().currentScriptable.colorType == ballValue.colorType)
            {
                body.isKinematic = false;
                body.AddForce(Vector3.forward * 1000f);
                Invoke("SetActiveFalse", 0.2f);
            }
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (collision.gameObject.GetComponent<GridBallBehavior>().ballValue.colorType == ballValue.colorType)
            {
                body.isKinematic = false;
                body.AddForce(Vector3.forward * 1000f);
                Invoke("SetActiveFalse", 0.2f);
            }
        }
    }

    private void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateMaterial(Vector3 pos)
    {
        transform.position = pos;
        body.isKinematic = true;
        meshRenderer.material = ballValue.material;
    }
}
