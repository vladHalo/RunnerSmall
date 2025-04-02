namespace _1Core.Scripts.Game.Player.Stats
{
    public class IntStatManager : StatManagerBase<IntStat, int>
    {
        private void Start()
        {
            LoadAllStats();
        }
    }
}