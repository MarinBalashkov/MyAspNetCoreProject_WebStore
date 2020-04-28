namespace WebStore.Services.Data
{
    using System.Threading.Tasks;

    public interface IRequestsToUsService
    {
         Task AddAsync(string name, string email, string title, string content);
    }
}
