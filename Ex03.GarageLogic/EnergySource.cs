using System;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        public enum eEnergyType
        {
            Fuel = 1,
            Electric
        } 

        protected float m_CurrentEnergyAmount;
        protected readonly float r_MaxEnergyCapacity;
        private float m_EnergyPercent;

        public EnergySource(float i_MaxEnergyCapacity, float i_CurrentEnergyAmount)
        {
            if (i_CurrentEnergyAmount > i_MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(0, i_MaxEnergyCapacity);
            }

            m_CurrentEnergyAmount = i_CurrentEnergyAmount;
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
            UpadateEnergyPercent();
        }

        public void EnergyChargeUp(float i_AmountOfEnergy)
        {
            if (m_CurrentEnergyAmount + i_AmountOfEnergy > r_MaxEnergyCapacity || m_CurrentEnergyAmount + i_AmountOfEnergy < 0)
            {
                throw new ValueOutOfRangeException(0, r_MaxEnergyCapacity);
            }

            m_CurrentEnergyAmount += i_AmountOfEnergy;
            UpadateEnergyPercent();
        }

        public float CurrentEnergyAmount
        {
            get
            {
                return m_CurrentEnergyAmount;
            }
        }

        public float MaxEnergyCapacity
        {
            get
            {
                return r_MaxEnergyCapacity;
            }
        }

        public static string[] GetEnergySrcOptions()
        {
            return Enum.GetNames(typeof(eEnergyType));
        }

        public void UpadateEnergyPercent()
        {
            m_EnergyPercent = (CurrentEnergyAmount / MaxEnergyCapacity) * 100;
        }

        public override string ToString()
        {
            return string.Format(
@"Energy percent: {0}", m_EnergyPercent);
        }
    }
}