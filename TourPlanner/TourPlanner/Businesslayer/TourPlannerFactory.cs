namespace TourPlanner.BusinessLayer
{
    public static class TourPlannerFactory
    {
        private static ITourPlannerFactory _instance;

        public static ITourPlannerFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TourPlannerFactoryImpl();
            }

            return _instance;
        }

    }
}
