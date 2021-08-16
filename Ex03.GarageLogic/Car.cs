using System;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Red = 1,
            Silver,
            White,
            Black,
        }

        public enum eNumOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five,
        }

        private const float k_FuelEnergyCapacity = 45;
        private const float k_ElectricEnergyCapacity = 3.2f;
        private const FuelEnergy.eFuelType k_FuelEnergyType = FuelEnergy.eFuelType.Octan95;
        private const float k_NumOfWheels = 4;
        private const float k_MaxAirPressure = 32;
        private readonly eColor r_Color;
        private readonly eNumOfDoors r_NumOfDoors;

        public Car(string i_ModelName, string i_LicenseNumber, Car.eColor i_Color, eNumOfDoors i_NumOfDoors)
            : base(i_ModelName, i_LicenseNumber)
        {
            r_Color = i_Color;
            r_NumOfDoors = i_NumOfDoors;
        }

        public static string[] GetCarColors()
        {
            return Enum.GetNames(typeof(eColor));
        }

        public static string[] GetCarNumberOfDoors()
        {
            return Enum.GetNames(typeof(eNumOfDoors));
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
Car color: {1}
Number of doors: {2}", base.ToString(), r_Color,r_NumOfDoors);
        }
    }
}