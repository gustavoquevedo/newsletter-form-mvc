using NewsletterForm.Controllers;
using NewsletterForm.DAL;
using NewsletterForm.Models;
using NewsletterForm.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;
using System;

namespace NewsletterForm.Tests.Controllers
{
    [TestClass]
    public class SubscriptionControllerTest
    {
        private const string TEST_EMAIL = "gusk82@gmail.com";

        private NewsletterFormContext db;

        private Subscription testSubscriptionStandard;
        private Subscription testSubscriptionMin;
        private Subscription testSubscriptionMax;
        private Subscription testSubscriptionLessThanMin;
        private Subscription testSubscriptionEmpty;

        SubscriptionController controller;

        [TestInitialize]
        public void Init()
        {
            db = new NewsletterFormContext();
            controller = new SubscriptionController();

            testSubscriptionStandard = new Subscription()
            {
                EmailAddress = TEST_EMAIL,
                Source = Source.Advert,
                Reason = "I like your stuff",
                DateTime = DateTime.Now
            };

            testSubscriptionMin = new Subscription()
            {
                EmailAddress = TEST_EMAIL,
                Source = Source.WordOfMouth,
                DateTime = DateTime.Now
            };

            testSubscriptionMax = new Subscription()
            {
                EmailAddress = TEST_EMAIL,
                Source = Source.Other,
                SourceOther = "I ended up visiting your website by chance when I was googling random stuff",
                Reason = "Since I started reading your articles I cannot wait to read the next one",
                DateTime = DateTime.Now
            };

            testSubscriptionLessThanMin = new Subscription()
            {
                EmailAddress = TEST_EMAIL
            };

            testSubscriptionEmpty = new Subscription();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var itemsToDelete = db.Subscriptions
                    .Where(e =>e.EmailAddress == TEST_EMAIL);
            db.Subscriptions.RemoveRange(itemsToDelete);
            db.SaveChanges();
        }

        [TestMethod]
        public void TestMapping()
        {
            // Arrange
            var countBefore = db.Subscriptions.Count();

            // Act
            var result = controller.SignUp(testSubscriptionStandard) as ViewResult;

            var dbEntry = db.Subscriptions
                .OrderByDescending(e => e.ID)
                .FirstOrDefault();

            var countAfter = db.Subscriptions.Count();

            // Assert
            Assert.IsTrue(testSubscriptionStandard.Equals(dbEntry));

            Assert.AreEqual(countBefore + 1, countAfter);
        }

        [TestMethod]
        public void ReportAfterStandard()
        {
            // Arrange
            var resultBefore = controller.Report() as ViewResult;

            // Act
            controller.SignUp(testSubscriptionStandard);
            var resultAfter = controller.Report() as ViewResult;

            // Assert
            Assert.IsNotNull(resultBefore);
            Assert.IsNotNull(resultAfter);

            var reportBefore = resultBefore.Model as NewsletterFormReport;
            var reportAfter = resultAfter.Model as NewsletterFormReport;

            Assert.IsInstanceOfType(reportBefore, typeof(NewsletterFormReport));
            Assert.IsInstanceOfType(reportAfter, typeof(NewsletterFormReport));
        }

        [TestMethod]
        public void ReportAfterMin()
        {
            // Arrange
            var result = controller.Report() as ViewResult;
            var reportBefore = result.Model as NewsletterFormReport;

            // Act
            controller.SignUp(testSubscriptionMin);
            result = controller.Report() as ViewResult;
            var reportAfter = result.Model as NewsletterFormReport;

            // Assert
            Assert.IsTrue(
                reportAfter.Count == (reportBefore.Count + 1));

            Assert.IsTrue(
                reportAfter.AdvertCount == (reportBefore.AdvertCount));
            Assert.IsTrue(
                reportAfter.AdvertRatio == (reportBefore.AdvertRatio));
            Assert.IsTrue(
                reportAfter.WordOfMouthCount == (reportBefore.WordOfMouthCount) + 1);
            Assert.IsTrue(
                reportAfter.WordOfMounthRatio >= (reportBefore.WordOfMounthRatio ?? 0));
            Assert.IsTrue(
                reportAfter.OtherCount == (reportBefore.OtherCount));
            Assert.IsTrue(
                reportAfter.OtherRatio.Value >= (reportBefore.OtherRatio ?? 0));

            Assert.IsTrue(
                reportAfter.ReasonEnteredCount == (reportBefore.ReasonEnteredCount));
            Assert.IsTrue(
                reportAfter.ReasonEnteredRatio.Value <= (reportBefore.ReasonEnteredRatio ?? 0));
        }

        [TestMethod]
        public void ReportAfterMax()
        {
            // Arrange
            var result = controller.Report() as ViewResult;
            var reportBefore = result.Model as NewsletterFormReport;

            // Act
            controller.SignUp(testSubscriptionMax);
            result = controller.Report() as ViewResult;
            var reportAfter = result.Model as NewsletterFormReport;

            // Assert
            Assert.IsTrue(
                reportAfter.Count == (reportBefore.Count + 1));

            Assert.IsTrue(
                reportAfter.AdvertCount == (reportBefore.AdvertCount));
            Assert.IsTrue(
                reportAfter.AdvertRatio <= (reportBefore.AdvertRatio));
            Assert.IsTrue(
                reportAfter.WordOfMouthCount == (reportBefore.WordOfMouthCount));
            Assert.IsTrue(
                reportAfter.WordOfMounthRatio <= (reportBefore.WordOfMounthRatio));
            Assert.IsTrue(
                reportAfter.OtherCount == (reportBefore.OtherCount + 1));
            Assert.IsTrue(
                reportAfter.OtherRatio.Value >= (reportBefore.OtherRatio ?? 0));

            Assert.IsTrue(
                reportAfter.ReasonEnteredCount == (reportBefore.ReasonEnteredCount) + 1);
            Assert.IsTrue(
                reportAfter.ReasonEnteredRatio.Value >= (reportBefore.ReasonEnteredRatio ?? 0));
        }

        [TestMethod]
        public void ReportAfterEmpty()
        {
            // Arrange
            var result = controller.Report() as ViewResult;
            var reportBefore = result.Model as NewsletterFormReport;

            // Act
            db.Subscriptions.Add(testSubscriptionEmpty);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                db.Subscriptions.Remove(testSubscriptionEmpty);
            }
            finally
            {
                result = controller.Report() as ViewResult;
                var reportAfter = result.Model as NewsletterFormReport;

                // Assert
                Assert.IsTrue(
                    reportAfter.Count == (reportBefore.Count));

                Assert.IsTrue(
                    reportAfter.AdvertCount == (reportBefore.AdvertCount));
                Assert.IsTrue(
                    reportAfter.AdvertRatio <= (reportBefore.AdvertRatio));
                Assert.IsTrue(
                    reportAfter.WordOfMouthCount == (reportBefore.WordOfMouthCount));
                Assert.IsTrue(
                    reportAfter.WordOfMounthRatio <= (reportBefore.WordOfMounthRatio));
                Assert.IsTrue(
                    reportAfter.OtherCount == (reportBefore.OtherCount));
                Assert.IsTrue(
                    reportAfter.OtherRatio.Value <= (reportBefore.OtherRatio ?? 0));

                Assert.IsTrue(
                    reportAfter.ReasonEnteredCount == (reportBefore.ReasonEnteredCount));
                Assert.IsTrue(
                    reportAfter.ReasonEnteredRatio.Value == (reportBefore.ReasonEnteredRatio ?? 0));
            }
        }
    }
}
