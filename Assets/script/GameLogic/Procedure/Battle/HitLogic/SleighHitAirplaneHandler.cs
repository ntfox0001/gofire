namespace GoFire
{
    public static class SleighHitAirplaneHandler
    {
        public static void OnHit(HitData<Sleigh, Airplane> data)
        {
            data.BeHit.LifeCtrl.AddLife(data.Hit.LifeCtrl.GetLife());
        }
    }
}