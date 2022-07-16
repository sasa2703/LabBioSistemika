using Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    public interface IPlatePlacement
    {
        List<Well> FillPlate();
        void AddPlate(PlateSizeEnum plateSizeEnum, int id);

        void AddSamplesAndReagents(int numberOfReplicates, List<Sample> samplesToAdd, List<Reagent> reagentsToAdd);
    }
}
