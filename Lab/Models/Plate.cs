using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    public class Plate: IPlate
    {
        public PlateSizeEnum Size { get; private set; }
        public int Id { get; private set; }

        public Plate(PlateSizeEnum _Size, int _Id)
        {
            Size = _Size;
            Id = _Id;
        }
    }
}
