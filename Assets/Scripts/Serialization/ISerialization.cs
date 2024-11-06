namespace Assets.Scripts.Serialization
{
    public interface ISerialization
    {
        void Save<T>(string key, T source);
        T Load<T>(string key);
    }
}
