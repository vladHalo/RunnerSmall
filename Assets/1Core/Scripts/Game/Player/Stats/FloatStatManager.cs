namespace _1Core.Scripts.Game.Player.Stats
{
    public class FloatStatManager : StatManagerBase<FloatStat, float>
    {
        private void Start()
        {
            LoadAllStats();
        }
    }
}