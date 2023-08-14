namespace TddBank.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void New_account_should_have_zero_as_balance()
        {
            var ba = new BankAccount();

            Assert.Equal(0, ba.Balance);
        }

        [Fact]
        public void Deposit_should_add_to_Balance()
        {
            var ba = new BankAccount();

            ba.Deposit(12m);
            ba.Deposit(8m);

            Assert.Equal(20m, ba.Balance);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Deposit_a_negative_or_zero_value_should_throw_ArgumentException(decimal amount)
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Deposit(amount));
        }

        [Fact]
        public void Withdraw_should_substract_from_Balance()
        {
            var ba = new BankAccount();
            ba.Deposit(20m);

            ba.Withdraw(8m);
            ba.Withdraw(2m);

            Assert.Equal(10m, ba.Balance);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Withdraw_a_negative_or_zero_value_should_throw_ArgumentException(decimal amount)
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Withdraw(amount));
        }

        [Fact]
        public void Withdraw_more_than_balance_throws_InvaildOperationEx()
        {
            var ba = new BankAccount();
            ba.Deposit(20m);

            Assert.Throws<InvalidOperationException>(() => ba.Withdraw(21));
        }

        [Fact]
        public void Withdraw_to_zero_Balance()
        {
            var ba = new BankAccount();
            ba.Deposit(20m);

            ba.Withdraw(20m);

            Assert.Equal(0m, ba.Balance);
        }
    }
}