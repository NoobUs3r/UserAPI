using MediatR;
using UserAPI.Models;

namespace UserAPI.Queries
{
    public class GetAllUsersQuery : IRequest<ResponseList<User>>
    {
        public bool UseCache { get; }

        public GetAllUsersQuery(bool useCache)
        {
            UseCache = useCache;
        }
    }
}
