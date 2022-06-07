using System;
using UnityEngine;

namespace Menus_UI
{
    public class StolenAnim : MonoBehaviour
    {
        private void OnEnable()
        {
            LeanTween.move(this.gameObject, new Vector3(1169, 430, 0), 1.0f).setOnComplete(DisableMe);
        }

        private void OnDisable()
        {
            UIMgr.Instance.isWaiting = false;
            transform.position = new Vector3(510, 25, 0);
        }

        private void DisableMe()
        {
            gameObject.SetActive(false);
        }
    }
}
