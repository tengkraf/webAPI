using Framework.Exceptions;

namespace Business
{
    public class GenericValidator : IGenericValidator
    {
        public void ValidateEntityExists<T>(T entity, int idSearchedFor)
        {
            if (entity == null)
                throw new NotFoundException($"Id {idSearchedFor} does not exist.");
        }
    }

    public interface IGenericValidator
    {
        void ValidateEntityExists<T>(T entity, int idSearchedFor);
    }
}
