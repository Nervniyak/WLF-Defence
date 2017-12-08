using UnityEngine;

namespace Assets
{
    public class CameraController : MonoBehaviour
    {
        public Transform Player;
        public float ClampXLeft;
        public float ClampXRight;
        private Vector3 _offset;

        void Start()
        {
            _offset = transform.position - Player.position;
        }

        void LateUpdate()
        {
            var position = Player.position;
            position.x = Mathf.Clamp(position.x, ClampXLeft, ClampXRight);
            transform.position = position + _offset;
        }
    }
}