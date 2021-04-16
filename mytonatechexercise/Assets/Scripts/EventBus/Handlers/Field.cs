using UnityEngine;

namespace MyProject.Events
{
	public class Field : Handler<FieldCreateMessage>
	{
		[SerializeField]
		private int size = 12;

		protected override void HandleMessage(FieldCreateMessage message)
		{
			var childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(message.Field[i / size, i % size]);
			}
		}
	}
}