using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UserAPI.Data;
using UserAPI.Models;
using UserAPI.Queries;

namespace UserAPI.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Response<User>>
    {
        private readonly UserDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public GetUserByIdHandler(UserDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<Response<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            List<User>? users = request.UseCache ? _memoryCache.Get<List<User>>("users") : null;
            bool comesFromCache = true;

            if (users == null)
            {
                users = await _context.Users.ToListAsync();
                comesFromCache = false;

                if (request.UseCache)
                    _memoryCache.Set("users", users, TimeSpan.FromMinutes(1));
            }

            var user = users.Find(x => x.Id == request.Id);
            return new Response<User>(user, comesFromCache);
        }
    }
}
