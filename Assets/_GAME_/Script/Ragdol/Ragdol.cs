using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdol : MonoBehaviour
{
  void Start()
  {
    RagdolRigidbodiesClose(true);
    RagdollCollidersClose(false);
  }
  public void RagdolRigidbodiesClose(bool isActive)
  {
    Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
    foreach (var physicsOfChildObjects in rigidbodies)
    {
      physicsOfChildObjects.isKinematic = isActive;
    }
  }

  public void RagdollCollidersClose(bool isActive)
  {
    CapsuleCollider[] rigidbodies = GetComponentsInChildren<CapsuleCollider>();
    foreach (var colliderOfChildObjects in rigidbodies)
    {
      colliderOfChildObjects.enabled = isActive;
    }
  }

  public void ActivateOrDeactivateRagdoll(bool isRigiActive, bool isCollActive)
  {
    RagdolRigidbodiesClose(isRigiActive);
    RagdollCollidersClose(isCollActive);
  }
  
}
