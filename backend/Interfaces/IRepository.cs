namespace backend.Interfaces;
public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync();
    public Task<T?> GetAsync(int modelId);
    public Task<T> InsertAsync(T model);
    public Task<T?> UpdateAsync(int modelId, T model);
    public Task<bool> DeleteAsync(int modelId);
}