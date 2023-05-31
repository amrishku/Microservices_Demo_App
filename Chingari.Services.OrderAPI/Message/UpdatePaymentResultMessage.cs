namespace Chingari.Services.OrderAPI.Message
{
    public class UpdatePaymentResultMessage
    {
        public int OrderId { get; set; }
        public bool Status { get; set; }
        //public string Email { get; set; }
    }
}
