using Microsoft.AspNetCore.Mvc;
using RubiconmpProject.Infrastructure;

namespace RubiconmpProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RectangleController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public RectangleController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Success");
        }

        [HttpPost]
        public IActionResult SearchIntersectingRectangles([FromBody] double firstSegmentX, double firstSegmentY, double secondSegmentX, double secondSegmentY)
        {
            // Logic to search for intersecting rectangles in the database
            var intersectingRectangles = _context.Rectangles
                                            .Where(r => (r.FirstSegment < secondSegmentX && r.FirstSegment + r.Width > firstSegmentX && r.SecondSegment < secondSegmentY && r.SecondSegment + r.Height > firstSegmentY))
                                                .ToList();
            return Ok(intersectingRectangles);
        }
    }
}
