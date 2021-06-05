namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private string r_Manufacturer;
        private float m_CurrentAirPressure;

        public Wheel(string i_WheelManufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            if (i_CurrentAirPressure > i_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, i_MaxAirPressure);
            }

            r_Manufacturer = i_WheelManufacturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }   

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public void FillAirPressure(float i_AmountOfAirToFill)
        {
            if (m_CurrentAirPressure + i_AmountOfAirToFill > r_MaxAirPressure || m_CurrentAirPressure + i_AmountOfAirToFill < 0)
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure);
            }
            else
            {
                m_CurrentAirPressure += i_AmountOfAirToFill;
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Manufacturer: {0}
CurrentAirPressure: {1}
MaxAirPressure: {2}", r_Manufacturer, m_CurrentAirPressure, r_MaxAirPressure);
        }
    }
}