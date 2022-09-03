using MediatR;
using UserAPI.Models;

namespace UserAPI.Queries
{
    public class GetUserByIdQuery : IRequest<Response<User>>
    {
        public int Id { get; }
        public bool UseCache { get; }

        public GetUserByIdQuery(int id, bool useCache)
        {
            Id = id;
            UseCache = useCache;
        }
    }
}
