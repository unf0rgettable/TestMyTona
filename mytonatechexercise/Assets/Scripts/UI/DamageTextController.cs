using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshPro))]
    public class DamageTextController : MonoBehaviour
    {
        private TextMeshPro TMProText;

        public void SetHealth(float damage)
        {
            TMProText = GetComponent<TextMeshPro>();
            TMProText.text = damage.ToString();
            if (damage > 0)
            {
                TMProText.color = Color.green;
            }
            else if(damage < 0)
            {
                TMProText.color = Color.yellow;
            }
            else if(damage == 0)
            {
                TMProText.text = string.Empty;
            }
            TMProText.DOColor(new Color(0, 0, 0, 0), 2);
            transform.DOLocalMoveY(2, 2f).onComplete = () =>
            {
                Destroy(gameObject);
            };
        }
    }
}