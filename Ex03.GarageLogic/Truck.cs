using System;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const float k_FuelEnergyCapacity = 120;
        private const FuelEnergy.eFuelType k_FuelEnergyType = FuelEnergy.eFuelType.Soler;
        private const float k_NumOfWheels = 16;
        private const float k_MaxAirPressure = 26;
        private readonly bool r_DrivesDangerousMaterials;
        private readonly float r_MaxCargo;

        public Truck(string i_ModelName, string i_LicenseNumber, bool i_DrivesDangerousMaterials, float i_MaxCargo)
            : base(i_ModelName, i_LicenseNumber)
        {
            this.r_DrivesDangerousMaterials = i_DrivesDangerousMaterials;
            this.r_MaxCargo = i_MaxCargo;
        }

        public override void InitializeEnergySource(EnergySource.eEnergyType i_EnergyType, float i_CurrentEnergyAmount)
        {
            if (i_EnergyType.Equals(EnergySource.eEnergyType.Electric))
            {
                throw new ArgumentException("Electric truck is not supported.");
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
            string drivesDangerousMaterials = r_DrivesDangerousMaterials ? "Truck contains dangerous materials" : "Truck does not conatins dangerous materials";

            return string.Format(
@"{0}
================
{1}
Truck max cargo: {2}", base.ToString(), drivesDangerousMaterials, r_MaxCargo);
        }
    }
}