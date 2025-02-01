
namespace NerdStore.Modules.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}