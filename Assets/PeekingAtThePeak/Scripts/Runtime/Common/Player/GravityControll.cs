using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class GravityControll : MonoBehaviour
    {
        public string targetObjectName = "Player";
        private string tagName = "Gravity";
        [SerializeField]PlayerBehaiviour pb;
      
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag(tagName))
            {
                pb.Rigidbody2D.gravityScale = 0f;
                pb.Rigidbody2D.velocity = new Vector2(0,3);
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(tagName))
            {
                float newRotation = pb.Rigidbody2D.rotation + pb.RotaionVector * pb.CurrentRotationSpeed * Time.deltaTime * 0.5f;
                pb.Rigidbody2D.MoveRotation(newRotation);
            }
        }
    }
}
