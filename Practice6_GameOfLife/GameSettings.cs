using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice6_GameOfLife
{
    public class GameSettings
    {
        private FieldBorderRules _borderRules;
        private GameSettings()
        {
            
        }

        private static GameSettings _instance;

        public static GameSettings GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameSettings();
            }

            return _instance;
        }

        public FieldBorderRules BorderRules => _borderRules;


    }
}
