using MediatR;

namespace UserAPI.Commands
{
    public class DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
