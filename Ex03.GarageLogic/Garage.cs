using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Vehicle> m_GarageVehicles;

        public Garage()
        {
            m_GarageVehicles = new Dictionary<string, Vehicle>();
        }

        public bool IsVehicleInGarage(string i_LicenseNumberToFind)
        {
            foreach (char letter in i_LicenseNumberToFind)
            {
                if (!(char.IsLetterOrDigit(letter)))
                {
                    throw new FormatException("Given license number has a char which is not a digit and not a letter");
                }
            }

            return m_GarageVehicles.ContainsKey(i_LicenseNumberToFind);
        }

        public void AddVehicleToGarage(Vehicle i_VehicleToAdd)
        {
            m_GarageVehicles.Add(i_VehicleToAdd.LicenseNumber, i_VehicleToAdd);
        }

        public static string[] GetVehicleStatusOptions()
        {
            return Enum.GetNames(typeof(VehicleOwner.eVehicleStatus));
        }

        public List<string> GetAllLicenseNumbers()
        {
            List<string> licenses = new List<string>();

            foreach (KeyValuePair<string, Vehicle> vehicle in m_GarageVehicles)
            {
                licenses.Add(vehicle.Value.LicenseNumber);
            }

            return licenses;
        }

        public List<string> GetLicenseNumbersByStatus(VehicleOwner.eVehicleStatus i_Status)
        {
            List<string> licenses = new List<string>();

            foreach (KeyValuePair<string, Vehicle> vehicle in m_GarageVehicles)
            {
                if (vehicle.Value.Owner.VehicleStatus.Equals(i_Status))
                {
                    licenses.Add(vehicle.Value.LicenseNumber);
                }
            }

            return licenses;
        }

        public void ChangeStatus(string i_LicenseNumber, VehicleOwner.eVehicleStatus i_VehicleStatus)
        {
            if (!(IsVehicleInGarage(i_LicenseNumber))) // can we reuse?
            {
                throw new ArgumentException(string.Format("Vehicle with license number {0} cannot be found in garage", i_LicenseNumber));
            }

            m_GarageVehicles[i_LicenseNumber].Owner.VehicleStatus = i_VehicleStatus;
        }

        public void InflateWheelsToMaximum(string i_LicenseNumber)
        {
            if (!(IsVehicleInGarage(i_LicenseNumber)))
            {
                throw new ArgumentException(string.Format("Vehicle with license number {0} cannot be found in garage", i_LicenseNumber));
            }

            List<Wheel> Wheels = m_GarageVehicles[i_LicenseNumber].Wheels;
            foreach (Wheel wheel in Wheels)
            {
                wheel.FillAirPressure(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public void RefuelFuelBasedVehicle(string i_LicenseNumber, FuelEnergy.eFuelType i_FuelType, float i_AmountOfFuelToFill)
        {
            if (!(IsVehicleInGarage(i_LicenseNumber)))
            {
                throw new ArgumentException(string.Format("Vehicle with license number {0} cannot be found in garage", i_LicenseNumber));
            }

            FuelEnergy vehicleFuelEnergySource = m_GarageVehicles[i_LicenseNumber].VehicleEnergySource as FuelEnergy;
            if (vehicleFuelEnergySource == null)
            {
                throw new ArgumentException(string.Format("Cannot refuel - vehicle with license number {0} as it has electric energy",i_LicenseNumber));
            }

            vehicleFuelEnergySource.Refuel(i_FuelType, i_AmountOfFuelToFill);
        }

        public void ChargeElectricBasedVehicle(string i_LicenseNumber, int i_MinutesToCharge)
        {
            if (!(IsVehicleInGarage(i_LicenseNumber)))
            {
                throw new ArgumentException(string.Format("Vehicle with license number {0} cannot be found in garage", i_LicenseNumber));
            }

            ElectricEnergy vehicleElectricEnergySource = m_GarageVehicles[i_LicenseNumber].VehicleEnergySource as ElectricEnergy;
            if (vehicleElectricEnergySource == null)
            {
                throw new ArgumentException(string.Format("Cannot charge - vehicle with license number {0} as it has fuel energy", i_LicenseNumber));
            }

            vehicleElectricEnergySource.RechargeElectricPower(i_MinutesToCharge); 
        }

        public string DisplayVehicleInformation(string i_LicenseNumber)
        {
            if (!(IsVehicleInGarage(i_LicenseNumber)))
            {
                throw new ArgumentException(string.Format("Vehicle with license number {0} cannot be found in garage", i_LicenseNumber));
            }
            else
            {
                return m_GarageVehicles[i_LicenseNumber].ToString();
            }
        }
    }
}