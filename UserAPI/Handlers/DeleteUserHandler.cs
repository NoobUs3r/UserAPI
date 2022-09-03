using MediatR;
using UserAPI.Commands;
using UserAPI.Data;

namespace UserAPI.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly UserDbContext _context;

        public DeleteUserHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _context.Users.FindAsync(request.Id);

            if (userToDelete == null)
                return 0;

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
