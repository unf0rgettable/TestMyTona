using TMPro;
using UnityEngine;

namespace UI
{
    public class CharacterHealthBar : MonoBehaviour, IHPChangeListener
    {
        [SerializeField]
        private GameObject Bar;
        [SerializeField]
        private SpriteRenderer BarImg;
        [SerializeField]
        private TMP_Text Text;
        [SerializeField]
        private GameObject DamageTextPrefab;
        
        private Camera _camera;

        private Character character;
        
        private void Awake()
        {
            _camera = Camera.main;
            character = GetComponentInParent<Character>();
            character.AddListener(this);
        }

        private void Start()
        {
            UpdateHP(character.MaxHealth, 0);
        }

        public void OnDeath()
        {
            Bar.SetActive(false);
        }

        public void UpdateHP(float hp, float diff)
        {
            Debug.Log("UpdateHP" + character.gameObject.name);
            var frac = hp / character.MaxHealth;
            Text.text = $"{hp:####}/{character.MaxHealth:####}";
            BarImg.size = new Vector2(frac, BarImg.size.y);
            var pos = BarImg.transform.localPosition;
            pos.x = -(1 - frac) / 2;
            BarImg.transform.localPosition = pos;
            GameObject textDamage = Instantiate(DamageTextPrefab, BarImg.transform.position, Quaternion.identity, Bar.transform);
            textDamage.GetComponent<DamageTextController>().SetHealth(diff);
            if (hp <= 0)
            {
                Bar.SetActive(false);
            }
        }
        
        private void OnDestroy()
        {
            character.RemoveListener(this);
        }
    }
}