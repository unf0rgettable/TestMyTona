namespace MyProject.Events
{
	public class FieldCreateMessage : Message
	{
		// Bonus!
		// Incapsulate public fields where is needed with properties/methods 
		public bool[,] Field { get; private set; }

		public FieldCreateMessage(bool[,] field)
		{
			Field = field;
		}
	}
}