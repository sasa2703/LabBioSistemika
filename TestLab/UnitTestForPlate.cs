using Lab;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestLab
{
    public class Tests
    {
        PlatePlacement platePlacement;

        [OneTimeSetUp]
        public void setup()
        {
            platePlacement = new PlatePlacement();
        }

        IPlateAdding plate;
        List<Sample> samples;
        List<Reagent> reagents;
        PlateFactory plateFactory;

        private void SetPlate(PlateSizeEnum size, out IPlateAdding plate, out List<Sample> samples, out List<Reagent> reagents)
        {
            plateFactory = new PlateFactory();
            plate = plateFactory.SetPlate(size);
            plate.AddPlate(size, 1);
            samples = new List<Sample>();
            reagents = new List<Reagent>();
        }


        [Test]
        public void TestUniqueNameOfReagent()
        {
            SetPlate(PlateSizeEnum.WELLS_96,out plate,out samples,out reagents);
            var reagnet = new Reagent { Name = "reag"};
            var reagnetWithSameName = new Reagent { Name = "reag" };
            reagents.Add(reagnet);
            reagents.Add(reagnetWithSameName);
            var sample1 = new Sample { Name = "Sample1" };
            var sample2 = new Sample { Name = "Sample2" };
       
            samples.Add(sample1);
            samples.Add(sample2);

            Assert.Throws<Exception>(() => plate.AddSamplesAndReagents(1, samples, reagents));    
        }

        [Test]
        public void SetAllNullTo96Plate()
        {
            var platePlacement = new PlateFactory();
            var plate = platePlacement.SetPlate(PlateSizeEnum.WELLS_96);
            plate.AddSamplesAndReagents(1, null,null);
            var plateResult = plate.FillPlate();
            Assert.IsTrue(plateResult.TrueForAll(x => x.ReagentName == null && x.SampleName == null));
        }


        [Test]
        public void SetAllNullTo384Plate()
        {
            var platePlacement = new PlateFactory();
            var plate = platePlacement.SetPlate(PlateSizeEnum.WELLS_384);
            plate.AddSamplesAndReagents(1, null, null);
            var plateResult = plate.FillPlate();
            Assert.IsTrue(plateResult.TrueForAll(x => x.ReagentName == null && x.SampleName == null));
        }


        [Test]
        public void SetAllNotNullTo96Plate()
        {
            SetPlate(PlateSizeEnum.WELLS_96, out plate, out samples, out reagents);
            for (int i = 0; i < 96; i++)
            {
                reagents.Add(new Reagent() { Name = "Reag" + i });
                samples.Add(new Sample() { Name = "Sample" + i });
            }
            plate.AddSamplesAndReagents(1, samples, reagents);
            var plateResult = plate.FillPlate();
            Assert.IsTrue(plateResult.TrueForAll(x => x.ReagentName != null && x.SampleName != null));
        }


        [Test]
        public void SetAllNotNullTo384Plate()
        {
            SetPlate(PlateSizeEnum.WELLS_384, out plate, out samples, out reagents);

            for (int i = 0; i < 384; i++)
            {
                reagents.Add(new Reagent() { Name = "Reag" + i });
                samples.Add(new Sample() { Name = "Sample" + i });
            }
            plate.AddSamplesAndReagents(1, samples, reagents);
            var plateResult = plate.FillPlate();
            Assert.IsTrue(plateResult.TrueForAll(x => x.ReagentName != null && x.SampleName != null));
        }

        [Test]
        public void Set3x3on96Plate()
        {
            SetPlate(PlateSizeEnum.WELLS_96, out plate, out samples, out reagents);
            for (int i = 0; i < 9; i++)
            {
                reagents.Add(new Reagent() { Name = "Reag" + i });
                samples.Add(new Sample() { Name = "Sample" + i });
            }
            plate.AddSamplesAndReagents(1, samples, reagents);
            var plateResult = plate.FillPlate();
            Assert.IsTrue(plateResult.Exists(x => x.ReagentName != null && x.SampleName != null));
        }

        [Test]
        public void Set9x9on96Plate()
        {
            SetPlate(PlateSizeEnum.WELLS_96, out plate, out samples, out reagents);
            for (int i = 0; i < 9; i++)
            {
                reagents.Add(new Reagent() { Name = "Reag" + i });
                samples.Add(new Sample() { Name = "Sample" + i });
            }
            plate.AddSamplesAndReagents(9, samples, reagents);
            var plateResult = plate.FillPlate();
            Assert.IsTrue(plateResult.Exists(x => x.ReagentName != null && x.SampleName != null));
        }

        [Test]
        public void AddSamplesAndReagnesTo3Experiments()
        {
            SetPlate(PlateSizeEnum.WELLS_96, out plate, out samples, out reagents);
            for (int i = 1; i < 10; i++)
            {
                reagents.Add(new Reagent() { Name = "Reag" + i });
                samples.Add(new Sample() { Name = "Sample" + i });
            }

            plate.AddSamplesAndReagents(3, samples, reagents);
            var plateResult = plate.FillPlate();
        }

        [Test]
        public void AboveLimitOnPlate96()
        {          
            SetPlate(PlateSizeEnum.WELLS_96, out plate, out samples, out reagents);
            for (int i = 1; i < 10; i++)
            {
                reagents.Add(new Reagent() { Name = "Reag" + i });
                samples.Add(new Sample() { Name = "Sample" + i });
            }

            Assert.Throws<Exception>(() => plate.AddSamplesAndReagents(20, samples, reagents));
          
        }

        [Test]
        public void AboveLimitOnPlate384()
        {
            SetPlate(PlateSizeEnum.WELLS_384, out plate, out samples, out reagents);
            for (int i = 1; i < 10; i++)
            {
                reagents.Add(new Reagent() { Name = "Reag" + i });
                samples.Add(new Sample() { Name = "Sample" + i });
            }

            Assert.Throws<Exception>(() => plate.AddSamplesAndReagents(50, samples, reagents));
        }

        [Test]
        public void AboveLimitOnTwoPlate384()
        {
            SetPlate(PlateSizeEnum.WELLS_384, out plate, out samples, out reagents);
            plate.AddPlate(PlateSizeEnum.WELLS_384, 2);
            for (int i = 1; i < 10; i++)
            {
                reagents.Add(new Reagent() { Name = "Reag" + i });
                samples.Add(new Sample() { Name = "Sample" + i });
            }

            Assert.Throws<Exception>(() => plate.AddSamplesAndReagents(100, samples, reagents));
        }

    }
}