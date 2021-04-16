using System;
using Character;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CharacterHealthBar : MonoBehaviour
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

        private ICharacter character;
        
        private void Awake()
        {
            _camera = Camera.main;
            character = GetComponentInParent<ICharacter>();
            character.OnHPChange += OnHPChange;
        }

        private void Start()
        {
            OnHPChange(character.MaxHealth, 0);
        }

        public void OnDeath()
        {
            Bar.SetActive(false);
        }

        private void OnHPChange(float health, float diff)
        {
            var frac = health / character.MaxHealth;
            Text.text = $"{health:####}/{character.MaxHealth:####}";
            BarImg.size = new Vector2(frac, BarImg.size.y);
            var pos = BarImg.transform.localPosition;
            pos.x = -(1 - frac) / 2;
            BarImg.transform.localPosition = pos;
            GameObject textDamage = Instantiate(DamageTextPrefab, BarImg.transform.position, Quaternion.identity, Bar.transform);
            textDamage.GetComponent<DamageTextController>().SetHealth(diff);
            if (health <= 0)
            {
                Bar.SetActive(false);
            }
        }
    }
}