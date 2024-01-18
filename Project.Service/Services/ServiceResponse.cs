namespace Project.Service.Services
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool HasError { get; set; } = false;
        public bool IsSuccess { get; set; } = false;

        public List<string> ErrorMessages { get; set; } = new List<string>();
        public List<string> SuccessMessages { get; set; } = new List<string>();


        public void AddErrorMessage(string errorMessage)
        {
            HasError = true;
            ErrorMessages.Add(errorMessage);
        }
        public void AddSuccessMessage(string successMessage)
        {
            IsSuccess = true;
            SuccessMessages.Add(successMessage);
        }
    }
}
