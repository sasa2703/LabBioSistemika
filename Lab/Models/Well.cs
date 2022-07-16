using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Models
{
    public class Well
    {
        public string SampleName { get; set; }
        public string ReagentName { get; set; }

        public int PositionOnPlateX { get; set; }
        public int PositionOnPlateY { get; set; }

        public int PlateId { get; set; }

        public Well(string _SampleName, string _ReagentName, int _PositionOnPlateX, int _PositionOnPlateY, int _PlateId)
        {
            SampleName = _SampleName;
            ReagentName = _ReagentName;
            PositionOnPlateX = _PositionOnPlateX;
            PositionOnPlateY = _PositionOnPlateY;
            PlateId = _PlateId;
        }

    }
}
