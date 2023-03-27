using System;
using UnityEngine;
using Signals;
using Managers;
using Data.ValueObjects;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;

        #endregion

        #region Private Variables

        [ShowInInspector] private bool inCollision;
        [ShowInInspector] private GameObject lastCollision = null;
        [ShowInInspector] private Transform characterTransform;

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && !inCollision)
            {
                if (other.gameObject != lastCollision)
                {
                    other.transform.position = new Vector3(transform.position.x, other.transform.position.y,
                        transform.position.z);
                    transform.position += (Vector3.up * (other.transform.localScale.y / 2));
                    characterTransform.position += Vector3.up * 0.25f;
                    other.transform.parent = transform;
                    lastCollision = other.gameObject;

                    InputSignals.Instance.onDisableInput?.Invoke();
                }

                return;
            }
            else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
            {
                if (lastCollision != null)
                {
                    lastCollision.transform.parent = null;
                }

                inCollision = true;

                if (transform.childCount < 3)
                {
                    CoreGameSignals.Instance.onLevelFailed?.Invoke();
                }
                else
                {
                    float shrinkAmount = transform.childCount / 4.0f;
                    Vector3 newScale = transform.localScale - new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);
                    transform.DOScale(newScale, 0.5f);
                    if (other.gameObject.CompareTag("Wall"))
                    {
                        CoreGameSignals.Instance.onLevelFailed?.Invoke();
                    }
                    else // Multiplier
                    {
                        CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var transform1 = manager.transform;
            var position = transform1.position;
            Gizmos.DrawSphere(new Vector3(position.x, position.y - 1.2f, position.z + 1f), 1.65f);
        }

        public void OnReset()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            transform.localScale = Vector3.one;
            lastCollision = null;
            inCollision = false;
        }
    }
}