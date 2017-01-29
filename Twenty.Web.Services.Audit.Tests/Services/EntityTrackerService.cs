using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twenty.Domain;
using Twenty.Domain.Interfaces;

namespace Twenty.Web.Services.Audit.Tests.Services
{
    [TestClass]
    public class EntityTrackerServiceTest
    {
        [TestMethod]
        public void Audit()
        {
            //Arrange
            User user = new User()
            {
                FirstName = "Dan",
                LastName = "Parker",
                Age = 31,
                City = "Anaheim"
            };

            ITrackableEntity service = new EntityTrackerService();

            //Act
            service.Audit(user);

            //Assert
            Assert.AreEqual(3, user.CurrentPropertyValues.Count());
            Assert.AreEqual(1, user.CurrentFieldValues.Count());
        }

        [TestMethod]
        public void AuditAndCreateMessages()
        {
            // Arrange
            User user = new User()
            {
                FirstName = "Dan",
                LastName = "Parker",
                Age = 31,
                City = "Anaheim"
            };

            ITrackableEntity service = new EntityTrackerService();

            // Act
            service.Audit(user);

            // change a few properties
            user.FirstName = "Florian";
            user.LastName = "Bates";
            user.City = "Los angeles";

            // register changes
            service.Audit(user);

            // get messages 
            string propertiesChangedMessage = service.GetChangedPropertiesAuditMessage(user);
            string fieldsChangedMessage = service.GetChangedFieldsAuditMessage(user);

            // Assert
            //Assert.AreEqual(2, user.CurrentPropertyValues.Count());
            Assert.AreEqual(1, user.CurrentFieldValues.Count());
            Assert.IsFalse(String.IsNullOrWhiteSpace(propertiesChangedMessage));
            Assert.IsFalse(String.IsNullOrWhiteSpace(fieldsChangedMessage));
            Assert.AreEqual(propertiesChangedMessage, "property FirstName, original value: Dan, new value: Florian\r\nproperty LastName, original value: Parker, new value: Bates\r\n");
            Assert.AreEqual(fieldsChangedMessage, "field City, original value: Anaheim, new value: Los angeles\r\n");

        }
    }
}
