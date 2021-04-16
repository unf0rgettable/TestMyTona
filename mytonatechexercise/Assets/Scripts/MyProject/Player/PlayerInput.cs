using MyProject.Events;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject.Player
{
	public class PlayerInput : MonoBehaviour
	{
		[SerializeField] private Image aim;
		private Camera _camera;
		public PlayerCharacter playerCharacter;

		private void Awake()
		{
			EventBus.Sub(PlayerDeadHandler, EventBus.PLAYER_DEATH);
		}

		private void OnDestroy()
		{
			EventBus.Unsub(PlayerDeadHandler, EventBus.PLAYER_DEATH);
		}

		private void PlayerDeadHandler()
		{
			enabled = false;
		}

		private void Start()
		{
			_camera = Camera.main;
			Cursor.visible = false;
		}

		void Update()
		{
			Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			
			aim.transform.position = Input.mousePosition;
			Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

			Plane plane = new Plane(Vector3.up, Vector3.up * playerCharacter.transform.position.y);
			plane.Raycast(ray, out var enter);
			Vector3 aimPos = ray.GetPoint(enter);
			Vector3 aimInput = aimPos - playerCharacter.transform.position;

			bool fire = Input.GetKey(KeyCode.Mouse0);
			EventBus<PlayerInputMessage>.Pub(new PlayerInputMessage(
				move: moveInput.normalized,
				aim: new Vector2(aimInput.x, aimInput.z).normalized,
				fire: fire
			));
		}
	}
}