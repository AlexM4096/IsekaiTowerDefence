namespace Dungeon.Generation
{
    public abstract class DungeonGenerator
    {
        protected readonly DungeonGeneratorConfig Config;

        protected DungeonGenerator(DungeonGeneratorConfig config)
        {
            Config = config;
        }
        
        public abstract Dungeon GenerateDungeon();

        public bool TryGenerateDungeon(out Dungeon dungeon)
        {
            dungeon = GenerateDungeon();
            return dungeon != null;
        }
    }
}