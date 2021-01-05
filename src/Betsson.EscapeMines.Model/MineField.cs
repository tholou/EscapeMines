using System.Collections.Generic;

namespace Betsson.EscapeMines.Model
{
    public class MineField
    {
        public int[] Size { get; set; }

        public List<int[]> MinesLocations { get; set; }

        public int[] ExitPoint { get; set; }
    }
}
