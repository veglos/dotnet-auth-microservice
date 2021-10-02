using System.Threading.Tasks;

namespace Auth.Application.UseCases
{
    public interface IUseCase<R, T>
        where R : Response
        where T : Request
    {
        public Task<R> Execute(T request);
    }
}
