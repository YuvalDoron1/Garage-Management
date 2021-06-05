namespace Ex03.GarageLogic
{
    public class SupportedVehiclesCreator
    {
        public static Truck CreateTruck(string i_ModelName, string i_LicenseNumber, bool i_DrivesDangerousMaterials, float i_MaxCargo)
        {
            return new Truck(i_ModelName, i_LicenseNumber, i_DrivesDangerousMaterials, i_MaxCargo);
        }

        public static Car CreateCar(string i_ModelName, string i_LicenseNumber, Car.eColor i_CarColor, Car.eNumOfDoors i_NumberOfDoors)
        {
            return new Car(i_ModelName, i_LicenseNumber, i_CarColor, i_NumberOfDoors);
        }

        public static Motorcycle CreateMotorcycle(string i_ModelName, string i_LicenseNumber, Motorcycle.eLicenseType i_LicenseType, int i_EngineVolume)
        {
            return new Motorcycle(i_ModelName, i_LicenseNumber, i_LicenseType, i_EngineVolume);
        }
    }
}