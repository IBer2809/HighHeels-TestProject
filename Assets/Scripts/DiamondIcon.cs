using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondIcon : MonoBehaviour
{
    private void Start()
    {
        Vector3 distance = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(distance);
    }

    public IEnumerator MoveDiamondUp(Transform diamondMovingTarget)
    {
        float t = 0f;
        Vector3 startPos = transform.position;
        while (t <= 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, diamondMovingTarget.position, t);
            yield return null;
        }
        Destroy(gameObject);
        UIManager.Instance.UpdateDiamondScore();
    }
}
