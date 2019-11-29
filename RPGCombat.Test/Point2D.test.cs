using System;
using Xunit;
using FluentAssertions;

namespace RPGCombat.Test
{
    
    public class Point2DTest
    {
        [Fact]
        public void distance_between_2_points()
        {
            var a = new Point2D(13, 17);
            var b = new Point2D(16, 21);
            var distance = a.DistanceTo(b);
            distance.Should().Be(5);
        }


    }
}
