using MediatR;
using UserAPI.Commands;
using UserAPI.Data;
using UserAPI.Models;

namespace UserAPI.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response<User>>
    {
        private readonly UserDbContext _context;

        public CreateUserHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<Response<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(request.User);
            await _context.SaveChangesAsync(cancellationToken);
            return new Response<User>(request.User, false);
        }
    }
}
