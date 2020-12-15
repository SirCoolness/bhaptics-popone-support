namespace BhapticsPopOne.Haptics
{
    public class EffectLoop
    {
        private bool firstActiveBuff = true;

        private bool ConsumeEnergyDrinkStatus = false;
        
        private void PlayActiveBuff()
        {
            if (firstActiveBuff && ConsumeEnergyDrinkStatus)
                return;
            
            var active = Mod.Instance.Data.Players.LocalPlayerContainer?.playerBuff?.healthOverTimes;

            if (active == null)
                return;
            
            firstActiveBuff = false;

            if (active.Count < 1)
            {
                firstActiveBuff = false;
                Mod.Instance.Haptics.Player.TurnOff("ActiveBuff");
                return;
            }

            if (Mod.Instance.Haptics.Player.IsPlaying("ActiveBuff"))
                return;
            
            Mod.Instance.Haptics.Player.SubmitRegistered("ActiveBuff");
        }

        private int statusSlowdown = 0;
        private void UpdateStatus()
        {
            statusSlowdown++;
            
            if (statusSlowdown < 10)
            {
                return;
            }
            
            statusSlowdown = 0;
            ConsumeEnergyDrinkStatus = Mod.Instance.Haptics.Player.IsPlaying("ConsumeEnergyDrink");
        }
        
        public void FixedUpdate()
        {
            // UpdateStatus();
            // PlayActiveBuff();
        }
    }
}