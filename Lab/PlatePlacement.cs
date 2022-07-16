using Lab.Models;
using Lab.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab
{
    public class PlatePlacement
    {
        protected IPlate plate;
        protected List<IExperiment> experiments;
        protected List<IPlate> plates;

        public PlatePlacement()
        {
            experiments = new List<IExperiment>();
            plates = new List<IPlate>();
        }

        public virtual List<Well> FillPlate() { return null; }


        private int columnsOrder;
        protected List<Well> PlateInsert(int size, int rows, int columns, IExperiment experiment)
        {
            int columnId = 1;
            int rowId = 1;
            var wells = new List<Well>();
            if (experiment.SamplesInExperiment == null)
            {
                columnsOrder = columns;
                originalColumnsOrder = columns;
            }
            else
            {
                columnsOrder = experiment.SamplesInExperiment.Count / 2;
                originalColumnsOrder = columnsOrder;
            }

            for (int i = 1; i <= size; i++)
            {
                InsertInWells(wells, rows, experiment, ref columnId, ref rowId);
            }

            Util.ToJsonFile(wells);

            return wells;
        }

        protected void CheckForWrongSettup(int numberOfReplicates, List<Sample> samplesToAdd, List<Reagent> reagents)
        {
            if (samplesToAdd != null)
            {
                bool isUniqueSamples = samplesToAdd.Distinct().Count() == samplesToAdd.Count();
                if (!isUniqueSamples)
                    throw new Exception("Samples must be unique!");
            }

            var totalPlateSize = plates.Sum(x => (int)x.Size);

            if (numberOfReplicates * samplesToAdd.Count > totalPlateSize)
            {
                throw new Exception("Number of samples are above plate limit!");
            }

            if (reagents.GroupBy(x => x.Name).Count() != reagents.Count())
                throw new Exception("Reagents name must be unique!");
        }

        private int positionInSamplesAndReagens = 0;
        private int numberOfReplicate = 1;
        private int originalColumnsOrder;

        private void InsertInWells(List<Well> wells, int rows, IExperiment experiment, ref int columnId, ref int rowId)
        {

            if (experiment.SamplesInExperiment != null && (positionInSamplesAndReagens < experiment.SamplesInExperiment.Count || positionInSamplesAndReagens < experiment.ReagentsInExperiment.Count))
                AddSamplesAndReagensToWell(wells, experiment, columnId, rowId);
            else if (CheckForReplication(experiment))
                ResetPositionInSamplesAndReagens();
            else
                AddWellsWithNull(wells, columnId, rowId);

            columnId++;

            if (rowId < rows && columnId > columnsOrder)
            {
                rowId++;
                columnId = 1;
            }

            if (rowId == rows && columnId == columnsOrder)
            {
                columnsOrder = columnsOrder + originalColumnsOrder; 
                columnId++;
                rowId = 1;
            }
        }

        protected void AddSamplesAndReagensToWell(List<Well> wells, IExperiment experiment, int columnId, int rowId)
        {
            wells.Add(new Well(experiment.SamplesInExperiment[positionInSamplesAndReagens].Name, experiment.ReagentsInExperiment[positionInSamplesAndReagens].Name, columnId, rowId, 1));
            positionInSamplesAndReagens++;
        }

        private void AddWellsWithNull(List<Well> wells, int columnId, int rowId)
        {
            wells.Add(new Well(null, null, columnId, rowId, 1));
            positionInSamplesAndReagens++;
        }

        private void ResetPositionInSamplesAndReagens()
        {
            positionInSamplesAndReagens = 0;
            numberOfReplicate++;
        }     

        private bool CheckForReplication(IExperiment experiment) => positionInSamplesAndReagens == experiment.SamplesInExperiment.Count && numberOfReplicate < experiment.NumberOfReplicates;
    }
}
