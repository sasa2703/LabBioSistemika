using Lab.Models;
using System.Collections.Generic;

namespace Lab
{
    public interface IPlateAdding
    {
        List<Well> FillPlate();

        void AddPlate(PlateSizeEnum plateSizeEnum, int id);

        void AddSamplesAndReagents(int numberOfReplicates, List<Sample> samplesToAdd, List<Reagent> reagentsToAdd);
    }
}
