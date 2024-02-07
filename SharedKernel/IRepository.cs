namespace SharedKernel;

public interface IRepository<T> where T : class, IAggregateRoot { }