using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace RubiconmpProjectTest
{
    [TestFixture]
    public class RectangleControllerTest
    {
        [Test]
        public void SearchIntersectingRectangles_ValidInput_ReturnsOkResultWithRectangles()
        {
            // Arrange
            var mockDbContext = new Mock<RepositoryContext>();
            var controller = new RectangleController(mockDbContext.Object);

            var rectangles = new List<Rectangle>
            {
                new Rectangle { FirstSegment = 0, SecondSegment = 0, Width = 5, Height = 5 },
                new Rectangle { FirstSegment = 3, SecondSegment = 3, Width = 5, Height = 5 },
                // Add more rectangles as needed
            };

            mockDbContext.Setup(m => m.Rectangles).Returns(rectangles.AsQueryable());

            var firstSegmentX = 1;
            var firstSegmentY = 1;
            var secondSegmentX = 4;
            var secondSegmentY = 4;

            // Act
            var result = controller.SearchIntersectingRectangles(firstSegmentX, firstSegmentY, secondSegmentX, secondSegmentY);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            var intersectingRectangles = okResult.Value as List<Rectangle>;
            Assert.AreEqual(1, intersectingRectangles.Count);
        }

        [Test]
        public void SearchIntersectingRectangles_NoIntersectingRectangles_ReturnsOkResultWithEmptyList()
        {
            // Arrange
            var mockDbContext = new Mock<ApplicationDbContext>();
            var controller = new RectangleController(mockDbContext.Object);

            var rectangles = new List<Rectangle>
            {
                new Rectangle { FirstSegment = 0, SecondSegment = 0, Width = 5, Height = 5 },
            };

            mockDbContext.Setup(m => m.Rectangles).Returns(rectangles.AsQueryable());

            var firstSegmentX = 10;
            var firstSegmentY = 10;
            var secondSegmentX = 15;
            var secondSegmentY = 15;

            var result = controller.SearchIntersectingRectangles(firstSegmentX, firstSegmentY, secondSegmentX, secondSegmentY);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            var intersectingRectangles = okResult.Value as List<Rectangle>;
            Assert.IsEmpty(intersectingRectangles);
        }

    }
}
