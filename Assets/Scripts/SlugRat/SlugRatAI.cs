using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugRatAI : MonoBehaviour
{
    [SerializeField]
    private float maxRange;
    [SerializeField]
    private float speed;

    private Vector3 centerPos;
    private float nextTurn = 0f;

    void Start()
    {
        centerPos = transform.position;
        Turn();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextTurn)
        {
            if (Vector3.Distance(transform.position, centerPos) > maxRange)
            {
                Turn(true);
            } else
            {
                Turn();
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += -transform.forward * Time.deltaTime * speed;
    }

    void Turn(bool towardsCenter = false)
    {
        float newRot = 0f;
        if (towardsCenter)
        {
            Vector3 toCenter = transform.position - centerPos;
            newRot = Mathf.Atan2(toCenter.x, toCenter.z) * Mathf.Rad2Deg + Random.Range(-10f, 10f);
        } else
        {
            newRot = Random.Range(0f, 360f);
        }
        transform.eulerAngles = new Vector3(0, newRot, 0);

        nextTurn = Time.time + Random.Range(0.1f, 0.7f);
    }
}
