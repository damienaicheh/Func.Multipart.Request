using System.Collections.Generic;
using System.Threading.Tasks;
using Func.SendFile;
using Func.SendFile.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Company.Function
{
    public class AzFunctionSendFileTest
    {
        private readonly Mock<ILogger<AzFunctionSendFile>> _logger;
        private readonly AzFunctionSendFile _sut;

        public AzFunctionSendFileTest()
        {
            _logger = new Mock<ILogger<AzFunctionSendFile>>();

            _sut = new AzFunctionSendFile(_logger.Object);
        }

        [Fact]
        public async Task TestRun_DataIsNull_ReturnBadRequestObjectResult()
        {
            // Arrange
            var formCollection = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());
            var request = HttpHelper.CreateMultipartRequest(formCollection);

            // Act
            var result = (ObjectResult)await _sut.Run(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(result.StatusCode, 400);
            Assert.Equal(result.Value, Errors.DATA_NOT_PROVIDED);
        }

        [Fact]
        public async Task TestRun_FileIsNUll_ReturnBadRequestObjectResult()
        {
            // Arrange
            var formCollection = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()
            {
                { "data", "My data" },
            });
            var request = HttpHelper.CreateMultipartRequest(formCollection);

            // Act
            var result = (ObjectResult)await _sut.Run(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(result.StatusCode, 400);
            Assert.Equal(result.Value, Errors.FILE_NOT_PROVIDED);
        }

        [Fact]
        public async Task TestRun_AllDataProvidedCorrectly_ReturnOkObjectResult()
        {
            // Arrange
            var formFileCollection = new FormFileCollection();
            formFileCollection.Add(HttpHelper.CreateFormFile("myFile", "Assets/demo.png", "image/png"));

            var formCollection = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()
            {
                { "data", "My data" },
            }, formFileCollection);
            var request = HttpHelper.CreateMultipartRequest(formCollection);

            // Act
            var result = (ObjectResult)await _sut.Run(request);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(result.StatusCode, 200);
        }
    }
}