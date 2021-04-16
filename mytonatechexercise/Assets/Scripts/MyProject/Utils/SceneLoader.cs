using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyProject.Utils
{
	public class SceneLoader : MonoBehaviour
	{
		public void Load(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		public void Load(int index)
		{
			SceneManager.LoadScene(index);
		}
	}
}