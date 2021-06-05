using System;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A = 1,
            B1,
            AA,
            BB,
        }

        private const float k_ElectricEnergyCapacity = 1.8f;
        private const float k_FuelEnergyCapacity = 6;
        private const FuelEnergy.eFuelType k_FuelEnergyType = FuelEnergy.eFuelType.Octan98;
        private const float k_NumOfWheels = 2;
        private const float k_MaxAirPressure = 30;
        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineVolume;

        public Motorcycle(string i_ModelName, string i_LicenseNumber, eLicenseType i_LicenseType, int i_EngineVolume)
            : base(i_ModelName, i_LicenseNumber)
        {
            r_LicenseType = i_LicenseType;
            r_EngineVolume = i_EngineVolume;
        }

        public static string[] GetLicenseTypes()
        {
            return Enum.GetNames(typeof(eLicenseType));
        }

        public override void InitializeEnergySource(EnergySource.eEnergyType i_EnergyType, float i_CurrentEnergyAmount)
        {
            if (i_EnergyType.Equals(EnergySource.eEnergyType.Electric))
            {
                r_VehicleEnergySource = new ElectricEnergy(k_ElectricEnergyCapacity, i_CurrentEnergyAmount);
            }
            else if (i_EnergyType.Equals(EnergySource.eEnergyType.Fuel))
            {
                r_VehicleEnergySource = new FuelEnergy(k_FuelEnergyCapacity, i_CurrentEnergyAmount, k_FuelEnergyType);
            }
        }

        public override void InitializeWheels(string i_WheelManufacturer, float i_CurrentAirPressure)
        {
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                r_Wheels.Add(new Wheel(i_WheelManufacturer, i_CurrentAirPressure, k_MaxAirPressure));
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
================
Motorcycle license Type: {1}
Motorcycle engine volume: {2}", base.ToString(), r_LicenseType, r_EngineVolume);
        }
    }
}