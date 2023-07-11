namespace MusicPlatform.Services
{

    public static class ObjectResolverService
    {

        private static IServiceProvider _serviceProvider;
        private static bool isSet = false;

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            if (isSet)
            {
                throw new InvalidOperationException(nameof(SetServiceProvider) + " can be called only once");
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            _serviceProvider = serviceProvider;

            isSet = true;
        }


        public static T Resolve<T>()
        {

            if (!isSet)
            {
                throw new InvalidOperationException(nameof(Resolve) + " can be called only after " + nameof(SetServiceProvider) + " is called");
            }

            var service = _serviceProvider.GetService<T>();
            if (service == null)
            {
                throw new Exception("Service not found");
            }

            return service;
        }
    }
}
