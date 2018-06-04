using Unity;

namespace Farzin.Infrastructure.CrossCutting.Factories
{
    public class IOCFactory
    {
        private static IUnityContainer _unityContainer;

        public static void SetContainer(object unityContainer)
        {
            _unityContainer = (IUnityContainer)unityContainer;
        }
        public static T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }
        public static T Resolve<T>(string name)
        {
            return _unityContainer.Resolve<T>(name);
        }

    }
}
