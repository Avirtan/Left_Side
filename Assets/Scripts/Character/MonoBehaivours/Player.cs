using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ReferenceEntity _referenceEntity;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag.Contains("Ground") && _referenceEntity != null)
        {
            var onGroundPool = _referenceEntity.World.GetPool<Components.Player>();
            ref var onGround = ref onGroundPool.Get(_referenceEntity.Entity);
            onGround.IsOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag.Contains("Ground"))
        {
            var onGroundPool = _referenceEntity.World.GetPool<Components.Player>();
            ref var onGround = ref onGroundPool.Get(_referenceEntity.Entity);
            onGround.IsOnGround = false;
        }
    }
}
