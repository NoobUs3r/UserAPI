using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Commands;
using UserAPI.Data;

namespace UserAPI.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly UserDbContext _context;

        public UpdateUserHandler(UserDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != request.User.Id)
                return 0;

            _context.Entry(request.User).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
