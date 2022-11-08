using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionToGameObject : MonoBehaviour
{
    public float xOffset, yOffset, zOffset, localXOffset, localYOffset, localZOffset;
    Transform targetTransform;

    public void moveToGameobjectGlobalOffset(GameObject go)
    {
        targetTransform = go.transform;
        targetTransform.position = new Vector3(targetTransform.position.x + xOffset, targetTransform.position.y + yOffset, targetTransform.position.z + zOffset);
        gameObject.transform.position = targetTransform.position;
    }
    public void moveToGameobjectLocalOffset(GameObject go)
    {
        targetTransform = go.transform;
        targetTransform.localPosition = new Vector3(targetTransform.localPosition.x + xOffset, targetTransform.localPosition.y + yOffset, targetTransform.localPosition.z + zOffset);
        gameObject.transform.position = targetTransform.position;
    }
}
