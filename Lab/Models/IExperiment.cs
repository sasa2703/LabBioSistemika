using System.Collections.Generic;

namespace Lab
{
    public interface IExperiment
    {

        int NumberOfReplicates { get; }
        List<Sample> SamplesInExperiment { get;}
        List<Reagent> ReagentsInExperiment { get;}
    }
}