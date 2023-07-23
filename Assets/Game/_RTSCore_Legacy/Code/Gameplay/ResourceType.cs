namespace MetaRTS_Legacy
{
	public enum ResourceType
	{
		None          = 0,
		Grass         = 1,  // - Трава
		Wood          = 2,  // - Дерево
		Stone         = 3,  // - Камень
		Iron          = 4,  // - Металл
		BlueCrystal   = 5,  // - Синие кристаллы
		Gold          = 6,  // - Золото
		GreenGas      = 7,  // - Зеленый газ
		RedJelly      = 8,  // - Красное желе
		Protuberance  = 9,  // - Протуберанцы силы
		Darkness      = 10, // - Тьма
		Huerga        = 11, // - Всякая хуерга
		SomethingElse = 12  // - Что-нибудь ещё
	}

	public struct SourceItem
	{
		public ResourceType type;
		public int        amount;
	}

	public class SourceContainer
	{
		public int Capacity { get; private set; }
		
		public void AddSource(ResourceType type, int amount)
		{
			
		}
	}
}