using UnityEngine;

namespace MyProject.Player
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem VFX;
        public void Exp(Vector3 position)
        {
            var temp = Instantiate(VFX);
            temp.transform.position = position;
            Destroy(temp, 3f);
        }
    }
}