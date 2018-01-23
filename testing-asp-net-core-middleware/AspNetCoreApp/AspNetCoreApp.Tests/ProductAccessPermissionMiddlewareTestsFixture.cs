using AspNetCoreApp.Infrastructure;
using AspNetCoreApp.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;

namespace AspNetCoreApp.Tests
{
    public class ProductAccessPermissionMiddlewareTestsFixture
    {
        public Mock<DelegateMock> RequestDelegateMock { get; set; }
        public ProductAccessPermissionMiddleware Sut { get; set; }
        public HttpContext HttpContext { get; set; }
        public Mock<IProductService> ProductServiceMock { get; set; }

        public ProductAccessPermissionMiddlewareTestsFixture(string path)
        {
            RequestDelegateMock = new Mock<DelegateMock>();

            RequestDelegateMock
                .Setup(x => x.RequestDelegate(It.IsAny<HttpContext>()))
                .Returns(Task.FromResult(0));

            Sut = new ProductAccessPermissionMiddleware(RequestDelegateMock.Object.RequestDelegate);

            ProductServiceMock = new Mock<IProductService>();
            HttpContext = new DefaultHttpContext();
            HttpContext.Request.Path = path;
        }

        public interface DelegateMock
        {
            Task RequestDelegate(HttpContext context);
        }
    }
}
