using MyProject.Data;

namespace MyProject.Events
{
	public class FieldCreateMessage : Message
	{
		public bool[,] Field { get; private set; }
		
		public FieldCreateMessage(LevelData level)
		{
			Field = level.GetMap();
		}
	}
}