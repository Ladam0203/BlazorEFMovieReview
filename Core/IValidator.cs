namespace Entities;

public interface IValidator<T>
{
    public IResponse Validate(T t);
}