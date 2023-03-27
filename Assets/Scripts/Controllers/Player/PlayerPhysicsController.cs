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

        [ShowInInspector] private float speed;

        [ShowInInspector] private GameObject lastCollision = null;
        [ShowInInspector] private Transform characterTransform;

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pickup") && !inCollision)
            {
                if (other.gameObject != lastCollision)
                {
                    // Toplanan küpü karakterin üzerine taşıyın ve oyuncunun yüksekliğini ayarlayın
                    other.transform.position = new Vector3(transform.position.x, other.transform.position.y,
                        transform.position.z);
                    transform.position += (Vector3.up * (other.transform.localScale.y / 2));
                    characterTransform.position += Vector3.up * 0.25f;
                    other.transform.parent = transform;
                    lastCollision = other.gameObject;

                    // Toplama işlemi tamamlandığında InputSignals'i devre dışı bırakın
                    InputSignals.Instance.onDisableInput?.Invoke();
                }

                return;
            }
            else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
            {
                // Engellere veya duvarlara çarpıldığında
                var thisCollider = other.contacts[0].thisCollider.transform;

                // Yere düşmeden önce son toplanan küpü serbest bırakın
                if (lastCollision != null)
                {
                    lastCollision.transform.parent = null;
                }

                inCollision = true;

                // Küp sayısı 3'ten azsa karakter kaybeder
                if (transform.childCount < 3)
                {
                    CoreGameSignals.Instance.onLevelFailed?.Invoke();
                }
                else
                {
                    // Küp sayısı 3 veya daha fazlaysa küp sayısı kadar küçülmeli
                    var shrinkAmount = transform.child;
                    Count / 4.0f;
                    var newScale = transform.localScale - new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);
                    transform.DOScale(newScale, 0.5f);
                    // Engellere çarptığında kaybetme animasyonunu oynatın
                    if (other.gameObject.CompareTag("Wall"))
                    {
                        CoreGameSignals.Instance.onLevelFailed?.Invoke();
                    }
                    else // Obstacle
                    {
                        CoreGameSignals.Instance.onLevelSuccessful();
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            // Hedef toplama noktasının yeri için bir gizmo çiz
            Gizmos.color = Color.yellow;
            var transform1 = manager.transform;
            var position = transform1.position;
            Gizmos.DrawSphere(new Vector3(position.x, position.y - 1.2f, position.z + 1f), 1.65f);
        }

        public void OnReset()
        {
            // Oyun sıfırlandığında tüm çocuk nesneleri yok edin ve boyutunu sıfırlayın
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            transform.localScale = Vector3.one;
            lastCollision = null;
            inCollision = false;
        }
    }