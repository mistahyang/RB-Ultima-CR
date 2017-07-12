using System.Threading.Tasks;

namespace UltimaCR.Rotations
{
    public sealed partial class Bard : Rotation
    {
        public override async Task<bool> Combat()
        {
            if (Ultima.UltSettings.SmartTarget)
            {
                if (await PitchPerfect()) return true;
                if (await Songs()) return true;
                if (await IronJaws()) return true;
		        if (await StraighterShotWMspec()) return true;
                if (await StraightShot()) return true;
            	if (await Sidewinder()) return true;
                if (await EmpyrealArrow()) return true;
            	if (await MiserysEnd()) return true;
		        if (await RainOfDeath()) return true;
                if (await Bloodletter()) return true;
                if (await QuickNock()) return true;
                if (await VenomousBite()) return true;
                if (await Windbite()) return true;
                if (await CausticBite()) return true;
                if (await Stormbite()) return true;
                return await HeavyShot();
            }
            if (Ultima.UltSettings.SingleTarget)
            {
                if (await StraighterShotWMspec()) return true;
                if (await StraightShot()) return true;
                if (await PitchPerfect()) return true;
                if (await Songs()) return true;
                if (await FoeRequiem()) return true;
                if (await RagingStrikes()) return true;
                if (await BattleVoice()) return true;
                if (await IronJaws()) return true;
                if (await Sidewinder()) return true;
                if (await Barrage()) return true;
                if (await EmpyrealArrow()) return true;
            	if (await MiserysEnd()) return true;
                if (await RainOfDeath()) return true;
                if (await Bloodletter()) return true;
                if (await Windbite()) return true;
                if (await VenomousBite()) return true;
                if (await CausticBite()) return true;
                if (await Stormbite()) return true;
                return await HeavyShot();
            }
            if (Ultima.UltSettings.MultiTarget)
            {
                if (await PitchPerfect()) return true;
                if (await Songs()) return true;
                if (await IronJaws()) return true;
		        if (await StraighterShotWMspec()) return true;
                if (await StraightShot()) return true;
            	if (await Sidewinder()) return true;
                if (await EmpyrealArrow()) return true;
            	if (await MiserysEnd()) return true;
		        if (await RainOfDeath()) return true;
                if (await Bloodletter()) return true;
                if (await QuickNock()) return true;
                if (await VenomousBite()) return true;
                if (await Windbite()) return true;
                if (await CausticBite()) return true;
                if (await Stormbite()) return true;
                return await HeavyShot();
            }
            return false;
        }

        public override async Task<bool> PVPRotation()
        {
            return false;
        }
    }
}