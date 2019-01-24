using NUnit.Framework;
using MannIsland.ModulusChecking.API.Services;


namespace MannIsland.ModulusChecking.API.Tests.Services
{
    //
    // Test the validation of the Modulus Weight
    // 5 Valid Y, 4 invalid N
    //
    class ModulusWeightServiceTest
    {
        // PASS Y return
        [Test]
        public void ModuleWeight_MOD10Pass_ReturnsValidY()
        {
            var service = new ModulusWeightService();
            string result = service.Validate("089999", "66374958");
            Assert.AreEqual("Y", result.ToString());
        }
        // PASS Y return
        [Test]
        public void ModuleWeight_MOD11Pass_ReturnsValidY()
        {

            var service = new ModulusWeightService();
            string result = service.Validate("107999", "88837491");
            Assert.AreEqual("Y", result);
        }

        // PASS Y return
        [Test]
        public void ModuleWeight_MOD11PassandDBLALPass_ReturnsValidY()
        {

            var service = new ModulusWeightService();
            string result = service.Validate("202959", "63748472");
            Assert.AreEqual("Y", result);
        }

        // PASS Y return
        [Test]
        public void ModuleWeight_Exception4RemainderCheckDigitEqual_ReturnsValidY()
        {

            var service = new ModulusWeightService();
            string result = service.Validate("134020", "63849203");
            Assert.AreEqual("Y", result);
        }

        // PASS Y return
        [Test]
        public void ModuleWeight_Exception7PassStandardCheckFail_ReturnsValidY()
        {

            var service = new ModulusWeightService();
            string result = service.Validate("772798", "99345694");
            Assert.AreEqual("Y", result);
        }

        // Fail N return
        [Test]
        public void ModuleWeight_MOD11PassandDBLALFail_ReturnsInValidN()
        {

            var service = new ModulusWeightService();
            string result = service.Validate("203099", "66831036");
            Assert.AreEqual("N", result);
        }

        // Fail N return
        [Test]
        public void ModuleWeight_MOD11FailandDBLALPass_ReturnsInValidN()
        {

            var service = new ModulusWeightService();
            string result = service.Validate("203099", "58716970");
            Assert.AreEqual("N", result);
        }

        // Fail N return
        [Test]
        public void ModuleWeight_MOD10Fail_ReturnsInValidN()
        {

            var service = new ModulusWeightService();
            string result = service.Validate("089999", "66374959");
            Assert.AreEqual("N", result);
        }


        // Fail N return
        [Test]
        public void ModuleWeight_MOD11Fail_ReturnsInValidN()
        {

            var service = new ModulusWeightService();
            string result = service.Validate("107999", "88837493");
            Assert.AreEqual("N", result);
        }

    }
}
