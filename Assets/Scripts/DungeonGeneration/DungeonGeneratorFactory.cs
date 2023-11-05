using System.Collections.Generic;

namespace DungeonGeneration
{
    public enum DungeonGeneratorType : byte
    {
        BSP,
        SmartBSP,
    }
    
    public class DungeonGeneratorFactory
    {
        private readonly DungeonGeneratorConfig _config;
        private readonly Dictionary<DungeonGeneratorType, DungeonGenerator> _generators;
        
        public DungeonGeneratorFactory(DungeonGeneratorConfig config)
        {
            _config = config;

            _generators = new Dictionary<DungeonGeneratorType, DungeonGenerator>()
            {
                { DungeonGeneratorType.BSP, new BSPDungeonGenerator(_config) },
                { DungeonGeneratorType.SmartBSP, new SmartBSPDungeonGeneration(_config) }
            };
        }

        public DungeonGenerator Get(DungeonGeneratorType type) => _generators[type];
    }
}