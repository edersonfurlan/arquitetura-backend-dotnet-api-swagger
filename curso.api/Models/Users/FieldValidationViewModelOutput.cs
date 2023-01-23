namespace curso.api.Models.Users
{
    public class FieldValidationViewModelOutput
    {
        public IEnumerable<string> Errors { get; private set; }

        public FieldValidationViewModelOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
