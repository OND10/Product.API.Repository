namespace ProductAPI.VSA.Features.Products.Repository.Interface
{
    public interface IUnitofWork
    {
        Task<int> SaveChangesAsync();
    }
}
