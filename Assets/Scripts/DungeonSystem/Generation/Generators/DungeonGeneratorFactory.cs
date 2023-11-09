using System.Collections.Generic;
using DungeonSystem.Generation.Generators;

namespace DungeonSystem.Generation
{
    public enum DungeonGeneratorType : byte
    {
        BSP,
        SmartBSP,
    }
    
    public class DungeonGeneratorFactory
    {
        private DungeonGeneratorConfig _config;
        private Dictionary<DungeonGeneratorType, DungeonGenerator> _generators;

        public DungeonGeneratorFactory() {}
        
        public void UpdateConfig(DungeonGeneratorConfig config)
        {
            _config = config;

            _generators = new Dictionary<DungeonGeneratorType, DungeonGenerator>()
            {
                { DungeonGeneratorType.BSP, new BSPDungeonGenerator(_config) },
                { DungeonGeneratorType.SmartBSP, new SmartBSPDungeonGenerator(_config) }
            };
        }

        public DungeonGenerator Get(DungeonGeneratorType type) => _generators[type];
    }
}