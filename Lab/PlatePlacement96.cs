﻿using Lab.Models;
using System.Collections.Generic;

namespace Lab
{
    public class PlatePlacement96 : PlatePlacement, IPlateAdding
    {
        public void AddPlate(PlateSizeEnum plateSizeEnum, int id)
        {
            plate = new Plate(plateSizeEnum, id);
            plates.Add(plate);
        }
        public List<Well> FillPlate()
        {          
            foreach (var experiment in experiments)
            {
                return PlateInsert(96, 8, 12, experiment);
            }

            return null;
        }

        void IPlateAdding.AddSamplesAndReagents(int numberOfReplicates, List<Sample> samplesToAdd, List<Reagent> reagentsToAdd)
        {
            base.AddSamplesAndReagents(numberOfReplicates, samplesToAdd, reagentsToAdd);
        }
        
    }
}
