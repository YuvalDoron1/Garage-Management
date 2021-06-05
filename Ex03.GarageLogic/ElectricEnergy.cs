namespace Ex03.GarageLogic
{
    class ElectricEnergy : EnergySource
    {
        public ElectricEnergy(float i_MaxEnergyHoursCapacity, float i_EnergyHoursLeft) : base(i_MaxEnergyHoursCapacity, i_EnergyHoursLeft)
        {
        }

        public void RechargeElectricPower(float i_HoursToCharge)
        {
            base.EnergyChargeUp(i_HoursToCharge / 60); 
        }

        public override string ToString()
        {
            return string.Format(
@"Electric energy hours left: {0}
Electric energy hours capacity: {1}
{2}", m_CurrentEnergyAmount, r_MaxEnergyCapacity,base.ToString());
        }
    }
}