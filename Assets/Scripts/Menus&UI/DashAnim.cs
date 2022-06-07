using UnityEngine;

namespace Menus_UI
{
    public class DashAnim : MonoBehaviour
    {
        private void OnEnable()
        {
            LeanTween.move(this.gameObject, new Vector3(1804, 46, 0), 1.0f);
            LeanTween.scale(this.gameObject, Vector3.one, 1f);
        }

        private void OnDisable()
        {
            transform.position = new Vector3(995, 490, 0);
            transform.localScale = Vector3.one * 3;
        }
    }
}
