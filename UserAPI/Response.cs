namespace UserAPI
{
    interface IResponse
    {
        bool ComesFromCache { get; set; }
    }

    public class Response<T> : IResponse
    {
        public bool ComesFromCache { get; set; }
        public T Data { get; set; }

        public Response(T data, bool comesFromCache)
        {
            Data = data;
            ComesFromCache = comesFromCache;
        }
    }

    public class ResponseList<T> : IResponse
    {
        public bool ComesFromCache { get; set; }
        public List<T> Data { get; set; }

        public ResponseList(List<T> data, bool comesFromCache)
        {
            Data = data;
            ComesFromCache = comesFromCache;
        }
    }
}
