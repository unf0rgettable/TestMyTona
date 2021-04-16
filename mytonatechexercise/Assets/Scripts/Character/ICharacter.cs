using System;

namespace Character
{
    public interface ICharacter
    {
        public float MaxHealth { get; }
        public Action<float, float> OnHPChange { get; set; }
    }
}