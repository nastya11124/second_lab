using App2.CoreSpace;
using App2.CoreSpace.DataBase.Interfaces;
using App2.CoreSpace.Interfaces;
using Moq;


namespace TestApp2
{
    public class DataChangerTests
    {
        [Fact]
        public async Task AddTrack_TrackExists_ReturnsFalse()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repo => repo.CheckTrackExists(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(true);

            var dataChanger = new DataChanger(mockRepository.Object);

            // Act
            var result = await dataChanger.AddTrack("Author", "Track");

            // Assert
            Assert.False(result);
            mockRepository.Verify(repo => repo.AddTrack(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task AddTrack_TrackDoesNotExist_ReturnsTrue()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repo => repo.CheckTrackExists(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(false);
            mockRepository.Setup(repo => repo.AddTrack(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(true);

            var dataChanger = new DataChanger(mockRepository.Object);

            // Act
            var result = await dataChanger.AddTrack("Author", "Track");

            // Assert
            Assert.True(result);
            mockRepository.Verify(repo => repo.AddTrack("Author", "Track"), Times.Once);
        }

        [Fact]
        public async Task AddTrack_TrackDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repo => repo.CheckTrackExists(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(false);
            mockRepository.Setup(repo => repo.AddTrack(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(false);

            var dataChanger = new DataChanger(mockRepository.Object);

            // Act
            var result = await dataChanger.AddTrack("Author", "Track");

            // Assert
            Assert.False(result);
            mockRepository.Verify(repo => repo.AddTrack("Author", "Track"), Times.Once);
        }
        [Fact]
        public async Task DeleteTrack_TrackExists_ReturnsTrue()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repo => repo.CheckTrackExists(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(true);
            mockRepository.Setup(repo => repo.DeleteTrack(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(true);

            var dataChanger = new DataChanger(mockRepository.Object);

            // Act
            var result = await dataChanger.DeleteTrack("Author", "Track");

            // Assert
            Assert.True(result);
            mockRepository.Verify(repo => repo.DeleteTrack("Author", "Track"), Times.Once);
        }

        [Fact]
        public async Task DeleteTrack_TrackDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repo => repo.CheckTrackExists(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(false);

            var dataChanger = new DataChanger(mockRepository.Object);

            // Act
            var result = await dataChanger.DeleteTrack("Author", "Track");

            // Assert
            Assert.False(result);
            mockRepository.Verify(repo => repo.DeleteTrack(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task DeleteTrack_TrackExists_ReturnsFalse()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(repo => repo.CheckTrackExists(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(true);
            mockRepository.Setup(repo => repo.DeleteTrack(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(false);

            var dataChanger = new DataChanger(mockRepository.Object);

            // Act
            var result = await dataChanger.DeleteTrack("Author", "Track");

            // Assert
            Assert.False(result);
            mockRepository.Verify(repo => repo.DeleteTrack("Author", "Track"), Times.Once);
        }
    }
}
