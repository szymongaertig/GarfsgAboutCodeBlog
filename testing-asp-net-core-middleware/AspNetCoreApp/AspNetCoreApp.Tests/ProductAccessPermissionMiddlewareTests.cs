using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreApp.Tests
{
    public class ProductAccessPermissionMiddlewareTests
    {
        [Theory]
        [InlineAutoData("/products/{0}/some-other-parts")]
        [InlineAutoData("/products/{0}/properties")]
        [InlineAutoData("/products/{0}/categories")]
        [InlineAutoData("/products/{0}")]
        public async Task With_Authorized_User_Then_Invokes_Next_Middleware(
            string path,
            int productId)
        {
            // Given
            var fixture = new ProductAccessPermissionMiddlewareTestsFixture(string.Format(path, productId));

            fixture.ProductServiceMock
                .Setup(x => x.DoesLoggedUserHaveAccessForProduct(productId))
                .Returns(true);

            // When
            await fixture.Sut.Invoke(fixture.HttpContext, fixture.ProductServiceMock.Object);

            // Then
            fixture.RequestDelegateMock.Verify(x => x.RequestDelegate(It.IsAny<HttpContext>()), Times.Once);
        }

        [Theory]
        [InlineAutoData("/products/{0}/exchange-keys")]
        [InlineAutoData("/products/{0}")]
        public async Task With_Not_Authorized_User_Then_Does_Not_Invoke_Next_Middleware(
           string path,
           int productId)
        {
            // Given
            var fixture = new ProductAccessPermissionMiddlewareTestsFixture(string.Format(path, productId));
            fixture.ProductServiceMock.Setup(x => x.DoesLoggedUserHaveAccessForProduct(productId))
                .Returns(false);

            // When
            await fixture.Sut.Invoke(fixture.HttpContext, fixture.ProductServiceMock.Object);

            // Then
            fixture.RequestDelegateMock.Verify(x => x.RequestDelegate(It.IsAny<HttpContext>()), Times.Never);
        }

        [Theory]
        [InlineAutoData("/products/{0}/exchange-keys")]
        [InlineAutoData("/products/{0}")]
        public async Task With_Not_Authorized_User_Then_Returns_401_Status_Code(
         string path,
         int productId)
        {
            // Given
            var fixture = new ProductAccessPermissionMiddlewareTestsFixture(string.Format(path, productId));

            fixture.ProductServiceMock.Setup(x => x.DoesLoggedUserHaveAccessForProduct(productId))
                .Returns(false);

            // When
            await fixture.Sut.Invoke(fixture.HttpContext, fixture.ProductServiceMock.Object);

            // Then
            Assert.Equal(401, fixture.HttpContext.Response.StatusCode);
        }

        [Theory]
        [InlineAutoData("/some-other-resources/c611373d-3a51-4ea2-8f32-4261eb7fdb20/exchange-keys")]
        [InlineAutoData("/some-other-resources")]
        public async Task When_Path_Doesnt_Match_Then_Does_Not_Invoke_Check_Permission_Method(
            string path,
            int productId)
        {
            // Given
            var fixture = new ProductAccessPermissionMiddlewareTestsFixture(string.Format(path, productId));

            // When
            await fixture.Sut.Invoke(fixture.HttpContext, fixture.ProductServiceMock.Object);

            // Then
            fixture.ProductServiceMock
                .Verify(x => x.DoesLoggedUserHaveAccessForProduct(It.IsAny<int>()), Times.Never);
        }
    }
}
