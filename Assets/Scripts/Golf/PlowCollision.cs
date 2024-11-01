using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlowCollision : MonoBehaviour
{
    [SerializeField] private float force = 10;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other, this);
        if (other.rigidbody)
        {
            var contact = other.contacts[0];
            other.rigidbody.AddForce(-contact.normal * force, ForceMode.Impulse);
        }
    }
}
