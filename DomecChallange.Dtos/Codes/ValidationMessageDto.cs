namespace DomecChallange.Dtos.Codes
{
    [Serializable]
    public class ValidationMessageDto
    {
        public ValidationMessageDto()
        {
            IsValid = true;
            Message = string.Empty;
        }

        public bool IsValid { get; set; }
        public string Message { get; set; }
        public dynamic Model { get; set; }

        public void AppendString(string message)
        {
            if (Message == string.Empty)
            {
                Message += message;
            }
            else
            {
                Message += "<br/>" + message;
            }
        }
    }
}
