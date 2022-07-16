using Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    public class PlatePlacement384 : PlatePlacement, IPlatePlacement
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
                return PlateInsert(384, 16, 24, experiment);
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
