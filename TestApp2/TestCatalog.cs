using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using App2.CoreSpace;
using App2.CoreSpace.Interfaces;


namespace TestApp2
{
    public class CatalogTests
    {
        [Fact]
        public async Task AddTrackOperation_ReturnsTrue()
        {
            // Arrange
            var mockDataChanger = new Mock<IDataChanger>();
            mockDataChanger.Setup(dc => dc.AddTrack(It.IsAny<string>(), It.IsAny<string>()))
                           .ReturnsAsync(true);

            var catalog = new Catalog(mockDataChanger.Object, null);

            // Act
            var result = await catalog.AddTrackOperation("Author", "Track");

            // Assert
            Assert.True(result);
            mockDataChanger.Verify(dc => dc.AddTrack("Author", "Track"), Times.Once);
        }

        [Fact]
        public async Task AddTrackOperation_ReturnsFalse()
        {
            // Arrange
            var mockDataChanger = new Mock<IDataChanger>();
            mockDataChanger.Setup(dc => dc.AddTrack(It.IsAny<string>(), It.IsAny<string>()))
                           .ReturnsAsync(false);

            var catalog = new Catalog(mockDataChanger.Object, null);

            // Act
            var result = await catalog.AddTrackOperation("Author", "Track");

            // Assert
            Assert.False(result);
            mockDataChanger.Verify(dc => dc.AddTrack("Author", "Track"), Times.Once);
        }
        [Fact]
        public async Task ShowAllTracksOperation_ReturnsDictionary()
        {
            // Arrange
            var mockDataSearcher = new Mock<IDataSearcher>();
            var expectedResult = new Dictionary<string, List<string>>
            {
                { "Author1", new List<string> { "Track1", "Track2" } },
                { "Author2", new List<string> { "Track3" } }
            };

            mockDataSearcher.Setup(ds => ds.Search(It.IsAny<int>(), It.IsAny<int>()))
                            .ReturnsAsync(expectedResult);

            var catalog = new Catalog(null, mockDataSearcher.Object);

            // Act
            var result = await catalog.ShowAllTracksOperation(1, 10);

            // Assert
            Assert.Equal(expectedResult, result);
            mockDataSearcher.Verify(ds => ds.Search(1, 10), Times.Once);
        }
        [Fact]
        public async Task FilterTrackOperation_ReturnsDictionary()
        {
            // Arrange
            var mockDataSearcher = new Mock<IDataSearcher>();
            var expectedResult = new Dictionary<string, List<string>>
            {
                { "Author1", new List<string> { "Track1", "Track2" } }
            };

            mockDataSearcher.Setup(ds => ds.Search(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                            .ReturnsAsync(expectedResult);

            var catalog = new Catalog(null, mockDataSearcher.Object);

            // Act
            var result = await catalog.FilterTrackOperation("Author1", true, 1, 10);

            // Assert
            Assert.Equal(expectedResult, result);
            mockDataSearcher.Verify(ds => ds.Search("Author1", true, 1, 10), Times.Once);
        }
        [Fact]
        public async Task DeleteTrackOperation_ReturnsTrue()
        {
            // Arrange
            var mockDataChanger = new Mock<IDataChanger>();
            mockDataChanger.Setup(dc => dc.DeleteTrack(It.IsAny<string>(), It.IsAny<string>()))
                           .ReturnsAsync(true);

            var catalog = new Catalog(mockDataChanger.Object, null);

            // Act
            var result = await catalog.DeleteTrackOperation("Author", "Track");

            // Assert
            Assert.True(result);
            mockDataChanger.Verify(dc => dc.DeleteTrack("Author", "Track"), Times.Once);
        }

        [Fact]
        public async Task DeleteTrackOperation_ReturnsFalse()
        {
            // Arrange
            var mockDataChanger = new Mock<IDataChanger>();
            mockDataChanger.Setup(dc => dc.DeleteTrack(It.IsAny<string>(), It.IsAny<string>()))
                           .ReturnsAsync(false);

            var catalog = new Catalog(mockDataChanger.Object, null);

            // Act
            var result = await catalog.DeleteTrackOperation("Author", "Track");

            // Assert
            Assert.False(result);
            mockDataChanger.Verify(dc => dc.DeleteTrack("Author", "Track"), Times.Once);
        }
    }
}
