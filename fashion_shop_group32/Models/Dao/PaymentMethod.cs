namespace fashion_shop_group32.Models.Dao
{
    public class PaymentMethod
    {
        public string paymentMethodCode { get; set; }
        public string paymentMethodName { get; set; }

        public PaymentMethod(string paymentMethodCode, string paymentMethodName)
        {
            this.paymentMethodCode = paymentMethodCode;
            this.paymentMethodName = paymentMethodName;
        }
    }
}