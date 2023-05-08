namespace VeloMotoAPI.Utilities
{
    public class WC
    {

        private static IWebHostEnvironment _environment;

        public WC(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        public static string PathProductImage
        {
            get { return ( @"\images\product\"); }
        }


    }
}
