using System.Collections.Generic;

namespace Lab
{
    public class Experiment : IExperiment
    {
        public int NumberOfReplicates { get; private set; }
        public List<Sample> SamplesInExperiment { get; private set; }
        public List<Reagent> ReagentsInExperiment { get; private set; }

        public Experiment(int _NumberOfReplicates, List<Sample> _SamplesInExperiment, List<Reagent> _ReagentsInExperiment)
        {
            NumberOfReplicates = _NumberOfReplicates;
            SamplesInExperiment = _SamplesInExperiment;
            ReagentsInExperiment = _ReagentsInExperiment;
        }


    }
}
