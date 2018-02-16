using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class CameraManager : MonoBehaviour, ICameraFollow
    {
        [SerializeField] float _distance;
        [SerializeField] float _angle;
        [SerializeField] Transform _targetTransform;
        Vector3 _targetPosition;

        void LateUpdate()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            _targetPosition = _targetTransform.position;
            _targetPosition.y += 0.5f; // the middle of the tank

            float angle = Mathf.Deg2Rad * _angle;
            float sideA = Mathf.Sin(angle) * _distance;

            // Knowing hypotenuse, we can use Pythagorean Theorem
            float sideB = Mathf.Sqrt( Mathf.Pow(_distance, 2) - Mathf.Pow(sideA, 2) );
            Vector3 offset = new Vector3(0, sideA, 0);

            transform.position = _targetPosition + -_targetTransform.forward * sideB + offset;

			// Look at tank
			Vector3 rot = _targetPosition - transform.position;
            transform.rotation = Quaternion.LookRotation(rot);
        }
        
        public void SetAngle(float angle)
        {
            _angle = angle;
        }

        public void SetDistance(float distance)
        {
            _distance = distance;
        }

        public void SetTarget(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }
    }
}