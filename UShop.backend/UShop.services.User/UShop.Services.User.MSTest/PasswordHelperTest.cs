using UShop.Shared.Common;

namespace UShop.Services.User.MSTest
{
    [TestClass]
    public class PasswordHelperTest
    {
        [TestMethod]
        public void Crypt()
        {
            string pwd = "password123";
            string pwd_hash = PasswordHelper.HashPassword(pwd);
            bool result = PasswordHelper.VerifyPassword(pwd, pwd_hash);
            Console.WriteLine($"pwd_hash => {pwd_hash}");
            Console.WriteLine($"result => {result}");
            Assert.IsTrue(result, "ÃÜÂë¼ÓÃÜÑéÖ¤Ê§°Ü");
        }
    }
}