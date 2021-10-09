using System.Threading.Tasks;

namespace Auth.Application.UseCases
{
    public interface IUseCase<R, S>
        where R : Request
        where S : Response
    {
        public Task<S> Execute(R request);
    }
}
