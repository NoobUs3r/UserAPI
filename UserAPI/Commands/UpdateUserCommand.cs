using MediatR;
using UserAPI.Models;

namespace UserAPI.Commands
{
    public class UpdateUserCommand : IRequest<int>
    {
        public int Id { get; set; }
        public User User { get; set; }

        public UpdateUserCommand(int id, User user)
        {
            Id = id;
            User = user;
        }
    }
}
