namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired,
            PayedFor
        }

        private readonly string r_Name;
        private readonly string r_PhoneNumber;
        private eVehicleStatus m_VehicleStatus;

        public VehicleOwner(string i_Name, string i_PhoneNumber)
        {
            r_Name = i_Name;
            r_PhoneNumber = i_PhoneNumber;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Owner name: {0}
Owner phone number: {1}
The Vehicle Status is: {2}", r_Name, r_PhoneNumber, m_VehicleStatus);
        }
    }
}