namespace D36_AspNetCoreMvc2Introduction.Services
{
    public class Calculator8 : ICalculator
    {
        public decimal Calculate(decimal amount)
        {
            return amount + (amount * 8 / 100);
        }
    }
}
