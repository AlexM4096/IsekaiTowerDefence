namespace DungeonGeneration
{
    public abstract class DungeonGenerator
    {
        protected readonly DungeonGeneratorConfig Config;

        protected DungeonGenerator(DungeonGeneratorConfig config)
        {
            Config = config;
        }
        
        public abstract Dungeon GenerateDungeon();
    }
}