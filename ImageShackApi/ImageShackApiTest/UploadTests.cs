using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageShackApi;
using System.Diagnostics;

namespace ImageShackApiTest
{
    [TestClass]
    public class UploadTests
    {
        [TestMethod]
        public void UploadTest()
        {
            ImageShackUploader.ApiKey = "269EFIJL61c2b056e30d6c142b1714e26725e591";
            var result = ImageShackUploader.UploadImage(@"D:\sepi_prst\Pictures\20170221_111238.jpg");

            Assert.AreNotSame(result.Links.ImageLink, null);

        }
    }
}
