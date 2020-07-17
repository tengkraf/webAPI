using Business;
using DTO;
using Framework.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class GenericValidatorUTests
    {
        private IGenericValidator _genericValidator;

        [TestInitialize]
        public void TestInitialize()
        {
            _genericValidator = new GenericValidator();
        }

        [TestMethod]
        public void ValidateEntityExists_When_EntityExists_Then_NoExceptionIsThrown()
        {
            _genericValidator.ValidateEntityExists(new Order(), 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void ValidateEntityExists_When_EntityDoesNotExist_Then_ExceptionIsThrown()
        {
            _genericValidator.ValidateEntityExists((Order)null, 1);
        }
    }
}
