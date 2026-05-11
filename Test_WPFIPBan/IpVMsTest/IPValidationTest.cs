using Logic_IPBanUtility.Models;
using WPF_IPBanUtility;
using WPF_IPBanUtility.Properties;
using static WPF_IPBanUtility.IPInputViewModel;

namespace IpVMsTest
{
     [TestClass]
     public class IPValidationTest
     {
          private IPValidation CreateValidation(string[] existingIPs = null)
          {
               existingIPs ??= Array.Empty<string>();
               return new IPInputViewModel.IPValidation(
                    _ => existingIPs,
                    () => KeyNames.Whitelist,
                    () => "до білого списку"
               );
          }

          [TestMethod]
          public void IPValidate_WhenEmpty()
          {
               var validation = CreateValidation();
               validation.IpAddress = string.Empty;
               Assert.IsFalse(validation.IsValidIP);
          }
          [TestMethod]
          public void IPValidate_WhenInvalid_Letters()
          {
               var validation = CreateValidation();
               validation.IpAddress = "abc.def.ghi.jkl";
               Assert.IsFalse(validation.IsValidIP);
          }

          [TestMethod]
          public void IPValidate_WhenInvalid_TooLarge()
          {
               var validation = CreateValidation();
               validation.IpAddress = "256.256.256.256";
               Assert.IsFalse(validation.IsValidIP);
          }

          [TestMethod]
          public void IPValidate_WhenValid()
          {
               var validation = CreateValidation();
               validation.IpAddress = "127.0.0.1";
               Assert.IsTrue(validation.IsValidIP);
          }

          [TestMethod]
          public void IPValidate_WhenValid_Exists()
          {
               var validation = CreateValidation(new[] { "192.168.1.1", "10.0.0.1" });
               validation.IpAddress = "10.0.0.1";
               Assert.IsFalse(validation.IsValidIP);
          }
     }
}
