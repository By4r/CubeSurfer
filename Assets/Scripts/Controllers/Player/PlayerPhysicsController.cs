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

        [ShowInInspector] private bool _isOver;

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StageArea"))
            {
                InputSignals.Instance.onDisableInput?.Invoke();


                return;
            }
            else CoreGameSignals.Instance.onLevelFailed?.Invoke();


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
        }
    }
}