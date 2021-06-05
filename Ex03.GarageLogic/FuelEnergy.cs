using System;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : EnergySource
    {
        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        readonly eFuelType r_FuelType;

        public FuelEnergy(float i_MaxFuelCapacity, float i_CurrentFuelAmount, eFuelType i_FuelType) : base(i_MaxFuelCapacity, i_CurrentFuelAmount)
        {
            this.r_FuelType = i_FuelType;
        }

        public static string[] GetFuelTypes()
        {
            return Enum.GetNames(typeof(eFuelType));
        }

        public void Refuel (eFuelType i_FuelType, float i_AmountOfFuelToRefuel)
        {
            if (!(i_FuelType.Equals(r_FuelType)))
            {
                throw new ArgumentException(string.Format("Cannot refuel - vehicle fuel type is: {0}, and it cannot be filled with: {1}", r_FuelType, i_FuelType));
            }
            else
            {
                base.EnergyChargeUp(i_AmountOfFuelToRefuel);
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Current fuel amount: {0}
Max fuel capacity: {1}
Fuel type: {2}
{3}", m_CurrentEnergyAmount, r_MaxEnergyCapacity, r_FuelType, base.ToString());
        }
    }
}