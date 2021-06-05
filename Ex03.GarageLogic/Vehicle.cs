using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eVehicleType
        {
            Car = 1,
            Truck,
            Motorcycle
        }

        readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        private VehicleOwner m_Owner;
        protected readonly List<Wheel> r_Wheels;
        protected EnergySource r_VehicleEnergySource;

        public Vehicle(string i_ModelName, string i_LicenseNumber)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_Wheels = new List<Wheel>();
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        }

        public VehicleOwner Owner
        {
            get
            {
                return m_Owner;
            }

            set
            {
                m_Owner = value;
            }
        }

        public EnergySource VehicleEnergySource
        {
            get
            {
                return r_VehicleEnergySource;
            }
        }

        public static string[] GetVehicleTypes()
        {
            return Enum.GetNames(typeof(eVehicleType));
        }

        public void RegisterOwnerToCar(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Owner = new VehicleOwner(i_OwnerName, i_OwnerPhoneNumber);
        }

        public abstract void InitializeWheels(string i_WheelManufacturer, float i_CurrentAirPressure);

        public abstract void InitializeEnergySource(EnergySource.eEnergyType i_EnergyType, float i_CurrentEnergyAmount);

        public override string ToString()
        {
            return string.Format(
@"Vehicle info:
================
License number: {0}
Model name: {1} 
================
Owner details:
{2}
================
Wheels info:
{3}
================
Energy information:
{4}",
r_LicenseNumber, r_ModelName, m_Owner.ToString(), r_Wheels[0].ToString(), r_VehicleEnergySource.ToString()) ;
        }
    }
}