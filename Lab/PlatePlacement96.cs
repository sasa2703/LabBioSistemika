using Lab.Models;
using System.Collections.Generic;

namespace Lab
{
    class PlatePlacement96 : PlatePlacement, IPlatePlacement
    {
        public void AddPlate(PlateSizeEnum plateSizeEnum, int id)
        {
            plate = new Plate(plateSizeEnum, id);
            plates.Add(plate);
        }
        public override List<Well> FillPlate()
        {          
            foreach (var experiment in experiments)
            {
                return PlateInsert(96, 8, 12, experiment);
            }

            return null;
        }

        void IPlatePlacement.AddSamplesAndReagents(int numberOfReplicates, List<Sample> samplesToAdd, List<Reagent> reagentsToAdd)
        {
            CheckForWrongSettup(numberOfReplicates, samplesToAdd,reagentsToAdd);

            experiments.Add(new Experiment(numberOfReplicates, samplesToAdd, reagentsToAdd));
        }
        
    }
}
