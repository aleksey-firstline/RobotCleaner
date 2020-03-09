namespace RobotCleaner.Validators.Interfaces
{
    public interface IValidator<TModel>
    {
        void ThrowIfInvalid(TModel model);
    }
}