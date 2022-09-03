using MediatR;
using UserAPI.Models;

namespace UserAPI.Commands
{
    public class CreateUserCommand : IRequest<Response<User>>
    {
        public User User { get; set; }

        public CreateUserCommand(User user)
        {
            User = user;
        }
    }
}
