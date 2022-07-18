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

        private int positionInSamplesAndReagens = 0;
        private int numberOfReplicate = 1;
        private int originalColumnsOrder;
        private int initialColumn = 1;

        public PlatePlacement()
        {
            experiments = new List<IExperiment>();
            plates = new List<IPlate>();
        }    

        private int columnsOrder;
        protected List<Well> PlateInsert(int size, int rows, int columns, IExperiment experiment)
        {
            int columnId = 1;
            int rowId = 1;
            var wells = new List<Well>();
            SetColumnOrder(columns, experiment);
            FillWells(size, rows, experiment, ref columnId, ref rowId, wells);

            Util.ToJsonFile(wells);

            return wells;
        }

        private void FillWells(int size, int rows, IExperiment experiment, ref int columnId, ref int rowId, List<Well> wells)
        {
            for (int i = 1; i < size - 1; i++)
            {
                InsertInWells(wells, rows, experiment, ref columnId, ref rowId);
            }
        }

        private void SetColumnOrder(int columns, IExperiment experiment)
        {
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
        }

        protected void CheckForWrongSetup(int numberOfReplicates, List<Sample> samplesToAdd, List<Reagent> reagents)
        {
            if (samplesToAdd != null)
            {
                bool isUniqueSamples = samplesToAdd.Distinct().Count() == samplesToAdd.Count();
                if (!isUniqueSamples)
                    throw new Exception("Samples must be unique!");
            }

            var totalPlateSize = plates.Sum(x => (int)x.Size);

            if (samplesToAdd != null && numberOfReplicates * samplesToAdd.Count > totalPlateSize)
            {
                throw new Exception("Number of samples are above plate limit!");
            }

            if (reagents != null && reagents.GroupBy(x => x.Name).Count() != reagents.Count())
                throw new Exception("Reagents name must be unique!");
        }       

        private void InsertInWells(List<Well> wells, int rows, IExperiment experiment, ref int columnId, ref int rowId)
        {

            if (ShuldAddSampleToWell(experiment))
                AddSamplesAndReagensToWell(wells, experiment, columnId, rowId);
            else if (experiment.ReagentsInExperiment != null && CheckForReplication(experiment))
            {
                ResetPositionInSamplesAndReagens();
                AddSamplesAndReagensToWell(wells, experiment, columnId, rowId);
            }
            else
                AddWellsWithNull(wells, columnId, rowId);

            ControlColumnsAndRows(wells, rows, experiment, ref columnId, ref rowId);
        }

        private void ControlColumnsAndRows(List<Well> wells, int rows, IExperiment experiment, ref int columnId, ref int rowId)
        {
            columnId++;

            if (ShuldResetColumnID(rows, columnId, rowId))
            {
                rowId++;
                columnId = initialColumn;
            }

            if (ShuldResetRowID(wells, rows, columnId, rowId))
            {
                ResetRowID(wells, experiment, ref columnId, ref rowId);
            }
        }

        private void ResetRowID(List<Well> wells, IExperiment experiment, ref int columnId, ref int rowId)
        {
            if (positionInSamplesAndReagens < experiment.SamplesInExperiment.Count)
            {
                AddSamplesAndReagensToWell(wells, experiment, columnId, rowId);
                ResetPositionInSamplesAndReagens();
            }
            initialColumn = columnsOrder + 1;
            columnsOrder = columnsOrder + originalColumnsOrder;
            columnId = initialColumn;
            rowId = 1;
        }

        private bool ShuldAddSampleToWell(IExperiment experiment)
        {
            return experiment.SamplesInExperiment != null 
                   && (positionInSamplesAndReagens < experiment.SamplesInExperiment.Count || positionInSamplesAndReagens < experiment.ReagentsInExperiment.Count);
        }

        private bool ShuldResetRowID(List<Well> wells, int rows, int columnId, int rowId) => rowId == rows && columnId == columnsOrder && wells.Count() < (int)plate.Size - 1;

        private bool ShuldResetColumnID(int rows, int columnId, int rowId) => rowId < rows && columnId > columnsOrder;

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

        protected void AddSamplesAndReagents(int numberOfReplicates, List<Sample> samplesToAdd, List<Reagent> reagentsToAdd)
        {
            CheckForWrongSetup(numberOfReplicates, samplesToAdd, reagentsToAdd);

            experiments.Add(new Experiment(numberOfReplicates, samplesToAdd, reagentsToAdd));
        }

        private bool CheckForReplication(IExperiment experiment) => positionInSamplesAndReagens == experiment.SamplesInExperiment.Count && numberOfReplicate < experiment.NumberOfReplicates;
    }
}
