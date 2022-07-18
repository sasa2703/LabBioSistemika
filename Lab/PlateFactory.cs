namespace Lab
{
    public class PlateFactory
    {
        public IPlateAdding SetPlate(PlateSizeEnum size)
        {
            if (size == PlateSizeEnum.WELLS_96)
            {
                return new PlatePlacement96();
            }
            else if (size == PlateSizeEnum.WELLS_384)
            {
                return new PlatePlacement384();
            }
            else
                throw new System.Exception("Wrong plate size!");

        }
    }

}
